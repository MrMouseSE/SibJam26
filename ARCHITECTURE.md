# SibJam26 — архитектура проекта

## 1. Архитектурный стиль

Проект построен как набор независимых игровых систем с единым циклом обновления:

- Контракт системы: `IGameSystem` (`Initialize`, `UpdateSystem`, `DisposeSystem`).
- Контракт механики: `IGameMechanic` (`UpdateMechanic`, `DisposeMechanic`).
- Оркестратор: `GameSystemsHandler`.
- Базовый шаблон модуля: `System + Mechanic + Component`.

Идея: `System` решает, когда логика должна выполняться, `Mechanic` содержит поведение, `Component` хранит состояние и ссылки на контейнеры/вью.

## 2. Слои

### 2.1 Bootstrap / Composition Root

- Файл: `Assets/Scripts/GameStartupScripts/LoadGameComponent.cs`.
- Задачи:
  - создание `GameSystemsHandler`;
  - регистрация всех глобальных систем;
  - загрузка стартовых сцен;
  - первичная инициализация инвентаря/настроек;
  - запуск `InitializeSystems()`.

### 2.2 Runtime Orchestration

- Файл: `Assets/Scripts/ScenesOperatingScripts/GameSystemsHandler.cs`.
- Задачи:
  - хранение реестра систем (`Dictionary<Type, IGameSystem>`);
  - вызов `Initialize` у систем;
  - update-loop через `UniTask` (кадр за кадром);
  - завершение через `DisposeSystems`.

### 2.3 Scene Layer

- Интерфейс корня сцены: `ISceneRoot`.
- Менеджер загрузки/активации: `SceneLoadingHandler`.
- Сцены грузятся additively через Addressables и переключаются через `SetSceneActive`.

### 2.4 Domain Systems

Ключевые доменные кластеры:

- `CoffeeSystemsScripts` — core-геймплей (элементы, рецепт, кипячение, расчет результата, награды).
- `LevelsSystemScripts` — прогресс по уровням и завершение.
- `MainMenuSystems` — старт/выход.
- `ScoreViewScripts` — отображение очков/уровня.
- `CameraSystem`, `GameInputScripts`, `GameStateScripts` — инфраструктура.

### 2.5 Data / Assets

- Игровые данные (ScriptableObject): `Assets/Descriptions/*`.
- Контент (prefabs, sounds, textures): `Assets/Content/*`.
- Конфигурация загрузки: `Assets/AddressableAssetsData/*`.

## 3. Жизненный цикл приложения

1. Запускается `LoadGameComponent.Awake()`.
2. Создается `GameSystemsHandler`.
3. Регистрируются системы.
4. Аддитивно грузятся стартовые сцены.
5. Для каждой сцены вызывается `InitializeSceneSystems(...)` у её container/root.
6. Активируется `MainMenuScene`.
7. `GameSystemsHandler` непрерывно вызывает `UpdateSystem(...)` у всех систем.

## 4. Управление состоянием (state-driven flow)

Глобальное состояние хранит `GameStateSystem`. Большинство gameplay-систем работает только в конкретном `GameStates`.

Типичный пайплайн:

`StartGame -> DrawElements -> SelectElements -> CompareRecipe -> StartBoil -> BoilingCoffee -> CalculateValue -> ApproveRecipe -> CoffeeShow -> CompareLevelComplete -> RewardElements -> ChangeLevel -> UpdateLevelView`

Это дает предсказуемый порядок этапов и минимизирует конфликт логики между подсистемами.

## 5. Контракты IGameSystem и IGameMechanic

### 5.1 `IGameSystem`

Интерфейс: `Assets/Scripts/GameSystemsScripts/IGameSystem.cs`

- `Initialize(GameSystemsHandler handler)` — подготовка системы после регистрации.
- `UpdateSystem(GameSystemsHandler handler, float deltaTime)` — вызов каждый кадр из общего цикла.
- `DisposeSystem()` — освобождение ресурсов/подписок на уровне системы.

Роль: orchestration-layer. Система принимает решение, нужно ли запускать механику в текущем кадре (часто через проверку `GameState`).

### 5.2 `IGameMechanic`

Интерфейс: `Assets/Scripts/GameSystemsScripts/IGameMechanic.cs`

- `UpdateMechanic(GameSystemsHandler handler, float deltaTime)` — исполняет прикладную логику кадра.
- `DisposeMechanic()` — отписка от событий и cleanup внутри механики.

Роль: behavior-layer. Механика не управляет общим циклом сама, а вызывается системой.

### 5.3 Взаимодействие `System -> Mechanic -> Component`

- `System` проверяет условия (state/флаги/валидацию) и делегирует выполнение.
- `Mechanic` обновляет состояние, дергает доменные операции, меняет state при необходимости.
- `Component` хранит runtime-данные и ссылки на контейнеры/вью.

Преимущества:

- проще тестировать логику в `Mechanic`;
- меньше связности между кадром и доменной логикой;
- единообразный стиль для всех подсистем.

## 6. Паттерн модуля `System / Mechanic / Component`

### `System`

- Проверяет глобальные условия выполнения (обычно `CurrentGameState`).
- Дергает `Mechanic.UpdateMechanic(...)` по кадру при выполнении условий.

### `Mechanic`

- Содержит поведение (переходы, вычисления, реакции на UI-события).
- Может подписываться на события контейнеров и переключать state.
- Реализует `IGameMechanic`.

### `Component`

- Держит флаги, ссылки на контейнеры, runtime-данные.
- Избегает тяжелой бизнес-логики.

## 7. Сценовая композиция

Сцены предоставляют свои контейнеры/вью и связывают их с глобальными системами:

- Базовый класс: `SceneSystemsContainer`.
- Пример: `GameSystemContainer` и `MainMenuSystemContainer`.
- Метод `InitializeSceneSystems(...)` выполняет scene-specific wiring.

Таким образом, системы остаются глобальными, а UI/объекты сцен подключаются динамически при активации сцены.

## 8. Зависимости

Критичные технологические зависимости:

- Unity 6 (`6000.3.13f1`).
- URP (`com.unity.render-pipelines.universal`).
- Addressables (`com.unity.addressables`).
- Input System (`com.unity.inputsystem`).
- UniTask (`com.cysharp.unitask`).

## 9. Расширение архитектуры

Как добавить новую gameplay-систему:

1. Создать `NewFeatureSystem`, `NewFeatureMechanic`, `NewFeatureComponent`.
2. Реализовать `IGameSystem` и `IGameMechanic`.
3. Зарегистрировать систему в `LoadGameComponent`.
4. Привязать к нужному `GameStates` (или явному условию).
5. Если нужны scene-объекты/кнопки — связать через соответствующий scene container.

## 10. Технические риски

- `GameSystemsHandler` обходит `Dictionary<Type, IGameSystem>` в update-цикле: изменения коллекции во время обхода могут вызвать ошибку.
- Сильная связка через прямой `GetGameSystem(typeof(...))`: при росте проекта может потребоваться более явный слой зависимостей/фасадов.
- В Build Settings есть потенциально устаревшая запись `MainMenu.unity` при фактическом использовании `MainMenuScene.unity`.

## 11. Правило создания новых объектов

Все новые игровые объекты должны создаваться только через паттерн статических фабрик.

Правила:

- Нельзя создавать новые игровые объекты напрямую в произвольных системах/механиках.
- Создание должно быть вынесено в отдельные статические фабрики (единая точка создания).
- Все такие фабрики должны лежать в отдельной папке с названием `GameObjectFactories`.

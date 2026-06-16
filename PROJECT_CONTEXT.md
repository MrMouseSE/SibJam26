# SibJam26 — быстрый контекст проекта

Этот файл нужен как постоянный источник данных по проекту, чтобы не делать полный реанализ структуры каждый раз.

## 1) Кратко о проекте

- Тип: Unity-проект (C#), архитектура на базе набора игровых систем (`IGameSystem`) + механик/компонентов.
- Версия Unity: `6000.3.13f1` (Unity 6).
- Основной код: `Assets/Scripts`.
- Основной контент: `Assets/Content`, `Assets/Descriptions`, `Assets/Scenes`.

## 2) Структура репозитория (важное)

- `Assets/Scripts` — весь прикладной код.
- `Assets/Scenes` — сцены (`StartScene`, `MainMenuScene`, `GameScene`).
- `Assets/Descriptions` — ScriptableObject-описания рецептов/дней/анимаций и т.д.
- `Assets/Content` — префабы, текстуры, звуки, шейдеры, анимации.
- `Assets/AddressableAssetsData` — конфиг Addressables (группы сцен/звуков/локальных ассетов).
- `Packages/manifest.json` — ключевые пакеты проекта.
- `ProjectSettings/*` — настройки Unity/рендера/билда/инпута.

## 3) Зависимости и техстек

Ключевые пакеты из `Packages/manifest.json`:

- `com.unity.render-pipelines.universal` (`17.3.0`) — URP.
- `com.unity.addressables` (`2.9.1`) — загрузка сцен/контента.
- `com.unity.inputsystem` (`1.19.0`) — Input System.
- `com.cysharp.unitask` (git) — async/update-инфраструктура.
- `com.kefir.mirroring.github.coplaydev.unity-mcp` (`9.6.10`) — MCP-интеграция.

## 4) Точки входа и жизненный цикл

### 4.1 Старт проекта

- `Assets/Scripts/GameStartupScripts/LoadGameComponent.cs`
  - В `Awake()`:
  - инициализирует звук через `SoundInstancerController`,
  - создает `GameSystemsHandler`,
  - регистрирует все глобальные `IGameSystem`,
  - запускает загрузку стартовых сцен через `SceneLoadingHandler.LoadScenes(...)`,
  - инициализирует доступные элементы/инвентарь,
  - вызывает `InitializeSystems()`.

### 4.2 Менеджер систем

- `Assets/Scripts/ScenesOperatingScripts/GameSystemsHandler.cs`
  - хранит `Dictionary<Type, IGameSystem>`,
  - вызывает `Initialize(...)` у всех систем,
  - крутит update-цикл через `UniTask` и `UpdateSystem(...)`,
  - отключает системы через `DisposeSystems()`.

### 4.3 Контракты

- `Assets/Scripts/GameSystemsScripts/IGameSystem.cs` — общий интерфейс системы.
- `Assets/Scripts/ScenesOperatingScripts/ISceneRoot.cs` — API root-объекта сцены (видимость, init, камера).

## 5) Сцены и переключение

- Загрузка сцен: `Assets/Scripts/GameStartupScripts/SceneLoadingHandler.cs`
  - сцены грузятся additively через Addressables,
  - `SceneRoots` хранит map `sceneName -> (handle, ISceneRoot)`,
  - `SetSceneActive(sceneName)` включает только нужную сцену и выставляет её active.

- Константы имен сцен:
  - `Assets/Scripts/ScenesOperatingScripts/SceneNamesConst.cs`
  - `MainMenuScene`, `GameScene`.

- Старт после загрузки:
  - `LoadGameComponent.SetMenuSceneActive()` активирует `MainMenuScene` и переключает камеру.

## 6) Архитектурный паттерн кода

Повторяющийся шаблон в `GameSystemsScripts`:

- `*System.cs` — оркестрация и вход в update.
- `*Mechanic.cs` — логика поведения/реакции.
- `*Component.cs` — состояние/ссылки на контейнеры.

Быстрые метрики (по текущему состоянию репо):

- C#-файлов в `Assets/Scripts`: `158`
- `*System.cs` в `Assets/Scripts/GameSystemsScripts`: `29`
- `*Mechanic.cs`: `29`
- `*Component.cs`: `28`

## 7) Ключевые подсистемы (ориентиры)

- Камера: `GameSystemsScripts/CameraSystem/*`
- Глобальный стейт: `GameSystemsScripts/GameStateScripts/*`
- Инпут/скорость: `GameInputScripts/*`, `GameSpeedScripts/*`
- Кофе-геймплей:
  - `CoffeeSystemsScripts/ElementSystemScripts/*`
  - `CoffeeSystemsScripts/RecipeSystemScripts/*`
  - `CoffeeSystemsScripts/BoilingSystemScripts/*`
  - `CoffeeSystemsScripts/CalculateResultSystemScripts/*`
  - `CoffeeSystemsScripts/RewardElementsSystemScripts/*`
- Уровни/прогресс:
  - `LevelsSystemScripts/LevelHandlerScripts/*`
  - `LevelsSystemScripts/LevelCompleteScripts/*`
  - `LevelsSystemScripts/GameCompleteSystemScripts/*`
- UI/скор:
  - `ScoreViewScripts/InterfaceScoreScripts/*`
  - `ScoreViewScripts/LevelViewScripts/*`
- Меню:
  - `MainMenuSystems/StartGameSystemScripts/*`
  - `MainMenuSystems/ExitGameSystemScripts/*`

## 8) Таблица систем и состояний

Источник: `GameScripts/GameStates.cs` + проверки `CurrentGameState` в `*System.cs`.

| Система | Активное состояние (`GameStates`) |
|---|---|
| `StartGameSystem` | `StartGame` |
| `ElementsDrawSystem` | `DrawElements` |
| `SelectionButtonsSystem` | `SelectElements` |
| `ElementsRedrawSystem` | `SelectElements` |
| `ElementSelectionSystem` | `SelectElements` |
| `CompleteSelectionSystem` | `SelectElements` |
| `FillRecipeSystem` | `SelectElements` |
| `CompareRecipeSystem` | `CompareRecipe` |
| `StartBoilingSystem` | `StartBoil` |
| `BoilingProcessSystem` | `BoilingCoffee` |
| `CalculateResultSystem` | `CalculateValue` |
| `BoostersSystem` | `CalculateValue` |
| `ApproveRecipeSystem` | `ApproveRecipe` |
| `ShowCoffeeSystem` | `CoffeeShow` |
| `LevelCompleteSystem` | `CompareLevelComplete` |
| `RewardElementsSystem` | `RewardElements` |
| `LevelHandlerSystem` | `ChangeLevel` |
| `LevelViewSystem` | `UpdateLevelView` |
| `InterfaceScoreSystem` | работает почти всегда (кроме `StartGame`) |

Системы без явной state-проверки в `UpdateSystem` обычно либо инфраструктурные (`GameStateSystem`, `GameInputSystem`, `GameCameraSystem`), либо завязаны на внутренние флаги/контейнеры.

## 9) Где искать данные игры

- Рецепты/элементы кофе: `Assets/Descriptions/Coffee/*`
- Дни/уровни/ачивки: `Assets/Descriptions/Days/*`
- Параметры анимаций: `Assets/Descriptions/Animations/*`
- Префабы: `Assets/Content/Prefabs/*`
- Звуки: `Assets/Content/Sounds/*`

## 10) Важные наблюдения и риски

- В `ProjectSettings/EditorBuildSettings.asset` есть запись `Assets/Scenes/MainMenu.unity` (disabled), при этом в `Assets/Scenes` присутствует `MainMenuScene.unity`. Возможна устаревшая запись в Build Settings.
- `GameSystemsHandler` обновляет `Dictionary<Type, IGameSystem>` в цикле. Если во время итерации добавлять/удалять системы из другого кода, это потенциальный риск модификации коллекции во время обхода.

## 11) Как использовать этот файл в работе

Перед анализом проекта:

1. Прочитать этот файл целиком.
2. Открыть только целевые каталоги из раздела 7.
3. Для изменений в flow сцен — смотреть разделы 4-5 и файлы `LoadGameComponent`, `SceneLoadingHandler`, `SceneNamesConst`.
4. Для gameplay-фич — идти в соответствующий `*System/*Mechanic/*Component` пакет.

## 12) Когда обновлять этот файл

Обновлять `PROJECT_CONTEXT.md` при:

- добавлении/удалении систем (`IGameSystem`);
- изменении стартового bootstrap (`LoadGameComponent`, загрузка сцен);
- изменении имен/набора сцен;
- значимых изменениях структуры `Assets/Scripts` или `Descriptions/Content`;
- апдейте ключевых пакетов в `Packages/manifest.json`.

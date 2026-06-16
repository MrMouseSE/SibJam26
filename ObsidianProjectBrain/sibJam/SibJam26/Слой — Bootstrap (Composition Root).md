---
tags: [архитектура, слой, sibjam26]
type: note
---

# 🚀 Слой — Bootstrap (Composition Root)

↑ [[Слои проекта]]

## Что это
Единая точка запуска приложения и «сборки» всех зависимостей. Реализован классом `LoadGameComponent` (`MonoBehaviour`) в `Assets/Scripts/GameStartupScripts/LoadGameComponent.cs`. Это единственный `MonoBehaviour`, который вручную ставится на сцену запуска.

## Что делает
- Хранит ссылки на данные и ассеты, проставленные в инспекторе: `SoundObjectReference`, `ScenesLoadAtStart`, `GameAudioMixer`, `SettingsHandler`, `CameraContainer`, и набор Description-ассетов (`CoffeeDescription`, `BoostersDescription`, `RewardShopDescription`, `DaysAchievementsDescription`, `AnimationsDescription`).
- Создаёт корневой оркестратор `GameSystemsHandler`.
- Регистрирует все ~26 систем.
- Запускает загрузку сцен и инициализацию инвентаря.

## Как работает (`Awake`)
```csharp
public async void Awake()
{
    await SoundInstancerController.SetRefObjects(SoundObjectReference, GameAudioMixer);
    StaticElementFactory.CoffeeDescription = CoffeeDescription;
    _gameSystemsHandler = new GameSystemsHandler();

    _gameSystemsHandler.AddStateSystem(new GameStateSystem());
    _gameSystemsHandler.AddGameSystem(new GameCameraSystem(CameraContainer));
    // ... регистрация остальных систем ...

    SceneLoadingHandler.LoadScenes(ScenesLoadAtStart);
    SceneLoadingHandler.OnSceneLoaded += SetMenuSceneActive;

    foreach (var startAvailableElement in CoffeeDescription.StartAvailableElements)
        PlayerPrefs.SetInt(startAvailableElement.ElementName, CoffeeDescription.StartElementCount);

    ElementsStaticInventory.LoadCurrentAvailableElements(CoffeeDescription);
    _gameSystemsHandler.InitializeSystems();
}
```

Порядок:
1. Подготовка звука (`SoundInstancerController`).
2. Конфиг фабрики элементов (`StaticElementFactory.CoffeeDescription`).
3. Создание `GameSystemsHandler` (он сразу запускает свой update-loop).
4. Регистрация систем (`AddStateSystem` для стейт-системы, `AddGameSystem` для прочих).
5. Аддитивная загрузка стартовых сцен + подписка на `OnSceneLoaded`.
6. Первичный инвентарь: запись стартовых элементов в `PlayerPrefs`, загрузка доступных элементов.
7. `InitializeSystems()` — `Initialize` у всех систем.

## Пост-загрузка — `SetMenuSceneActive()`
Вызывается по событию `OnSceneLoaded`: отписывается, у каждого scene root вызывает `InitializeSceneSystems`, активирует `MainMenuScene`, привязывает камеру к holder сцены меню и включает `IsCameraUpdating`.

## Роль в архитектуре
Bootstrap — единственное место, где «всё знает обо всём»: он создаёт оркестратор, наполняет его системами и связывает с данными. Все остальные слои зависимостей напрямую не собирают.

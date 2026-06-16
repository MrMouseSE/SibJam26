---
tags: [архитектура, слой, sibjam26]
type: note
---

# 🎬 Слой — Scene Layer

↑ [[Слои проекта]]

## Что это
Слой управления сценами: грузит их через Addressables, активирует одну за раз и связывает объекты сцены с глобальными системами. Ключевые типы — `SceneLoadingHandler`, `ISceneRoot` / `SceneRootHolder`, `SceneSystemsContainer`.

## Что делает
- Аддитивно грузит стартовые сцены и параллельно ждёт их готовности.
- Хранит реестр загруженных сцен с их корнями.
- Включает/выключает объекты сцены (только одна активна).
- Запускает scene-specific связывание систем.

## Как работает

### Загрузка — `SceneLoadingHandler` (static)
```csharp
public static Dictionary<string, (AsyncOperationHandle<SceneInstance>, ISceneRoot)> SceneRoots = new();
public static Action OnSceneLoaded;

public static async void LoadScenes(AssetReference[] scenesLoadAtStart)
{
    Task[] tasks = new Task[scenesLoadAtStart.Length];
    for (var i = 0; i < scenesLoadAtStart.Length; i++)
        tasks[i] = LoadScenes(scenesLoadAtStart[i]);
    await Task.WhenAll(tasks);
    OnSceneLoaded?.Invoke();
}
```
Каждая сцена грузится `Addressables.LoadSceneAsync(..., LoadSceneMode.Additive, true)`; из корневого объекта достаётся `ISceneRoot`, кладётся в `SceneRoots`, и сразу `SetSceneObjectsActive(false)` (скрыта до активации).

### Активация
```csharp
public static ISceneRoot SetSceneActive(string sceneName)
{
    foreach (var sceneRoot in SceneRoots)
        sceneRoot.Value.Item2.SetSceneObjectsActive(sceneRoot.Key == sceneName);
    SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    // ...
}
```
Активной делается одна сцена, остальные гасятся.

### Корень сцены — `ISceneRoot` / `SceneRootHolder`
Интерфейс: `SetSceneObjectsActive`, `InitializeSceneSystems`, `GetCameraHolder`, плюс управление видимостью объектов (`AddObjectToSceneVisibility` / `TryRemoveObjectFromVisibility`).
`SceneRootHolder` (`MonoBehaviour`) держит `List<GameObject> VisibleObjects`, `CameraHolder`, `AreaMusicContainer SceneMusicContainer`, ссылку на `SceneSystemsContainer`. `SetSceneObjectsActive` переключает музыку сцены и активность всех видимых объектов; `InitializeSceneSystems` делегирует в контейнер.

### Связывание — `SceneSystemsContainer`
Базовый `MonoBehaviour` с виртуальным `InitializeSceneSystems(handler)`. Конкретные сцены переопределяют его (например `GameSystemContainer`) и прокидывают свои Unity-объекты в нужные системы.

## Роль в архитектуре
Отвязывает глобальные системы от конкретных сцен: системы живут постоянно, а сцены подключают к ним свои объекты при активации.

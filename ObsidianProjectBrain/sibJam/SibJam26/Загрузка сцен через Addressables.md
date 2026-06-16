---
tags: [приём, sibjam26]
type: note
---

# 📦 Загрузка сцен через Addressables

↑ [[Приёмы]]

## Суть
Сцены и часть ассетов грузятся через **Addressables** (`AssetReference`), а не прямыми ссылками/`SceneManager` по имени.

- `LoadGameComponent.ScenesLoadAtStart` — массив `AssetReference[]` стартовых сцен.
- `SceneLoadingHandler.LoadScenes(...)` грузит сцены **аддитивно**.
- Событие `SceneLoadingHandler.OnSceneLoaded` сигналит о завершении загрузки.
- Активная сцена выбирается через `SceneLoadingHandler.SetSceneActive(SceneNamesConst.MainMenuScene)`.
- `SceneLoadingHandler.SceneRoots` — словарь корней сцен (`ISceneRoot`), у каждого вызывается `InitializeSceneSystems`.
- Звук тоже через `AssetReference`: `SoundInstancerController.SetRefObjects(SoundObjectReference, ...)`.

## Зачем
- Аддитивная композиция: меню и игровая сцена живут параллельно, переключается активная.
- Контент подгружается по запросу, отвязан от билд-сцен напрямую.

---
tags: [паттерн, sibjam26]
type: note
---

# 🔌 Контейнеры сцены (Scene Containers)

↑ [[Паттерны]]

Реализация роли **Container** из паттерна проекта.

## Суть
Глобальные системы — чистые C#-классы без Unity-ссылок. Сцена при загрузке «прокидывает» свои Unity-объекты в нужные механики через контейнер.

- Базовый класс: `SceneSystemsContainer`.
- Примеры: `GameSystemContainer`, `MainMenuSystemContainer`.
- Метод `InitializeSceneSystems(GameSystemsHandler)` выполняет scene-specific wiring.

## Как работает (на `GameSystemContainer`)
Контейнер держит в инспекторе ссылки на UI/объекты сцены (`ShowCoffeeContainer`, `BoilCoffeeContainer`, кнопки, `InterfaceScoreContainer`, `RewardShopContainer` и т.д.). В `InitializeSceneSystems`:
```csharp
var drawSystem = (ElementsDrawSystem)systemsHandler.GetGameSystem(typeof(ElementsDrawSystem));
drawSystem.Component.Container = ElementsDrawHandlerContainer;

var selectionButtonsSystem = (SelectionButtonsSystem)systemsHandler.GetGameSystem(typeof(SelectionButtonsSystem));
selectionButtonsSystem.Mechanic.SetButtonsContainers(CompleteButtonContainer, RedrawButtonContainer, systemsHandler);
// ... и так для каждой системы, которой нужны объекты сцены
```
Каждая система получает свой контейнер либо в `Component.Container`, либо через `Mechanic.SetContainer(...)`.

## Зачем
- Системы остаются глобальными и переживают смену сцен.
- Unity-ссылки изолированы в контейнерах сцены.
- Подключение объектов происходит динамически при активации сцены.

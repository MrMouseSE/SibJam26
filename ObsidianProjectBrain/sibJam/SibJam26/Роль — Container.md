---
tags: [паттерн, роль, sibjam26]
type: note
---

# 🔌 Роль — Container

↑ [[Паттерн System-Mechanic-Component-Container]]

## Что это
Мост между чистой архитектурой и Unity. Контейнер — это `MonoBehaviour` на объекте сцены, который держит ссылки на конкретные Unity-компоненты (`Transform`, кнопки, аниматоры, рендеры).

## Что делает
- Изолирует Unity-ссылки от систем и механик.
- Является точкой подключения объектов сцены к глобальным системам.
- Прокидывается в механику/компонент при инициализации сцены.

## Как работает
Сцена через `SceneSystemsContainer.InitializeSceneSystems(handler)` достаёт нужные системы и отдаёт им контейнеры:
```csharp
var drawSystem = (ElementsDrawSystem)handler.GetGameSystem(typeof(ElementsDrawSystem));
drawSystem.Component.Container = ElementsDrawHandlerContainer;

var selectionButtons = (SelectionButtonsSystem)handler.GetGameSystem(typeof(SelectionButtonsSystem));
selectionButtons.Mechanic.SetButtonsContainers(CompleteButtonContainer, RedrawButtonContainer, handler);
```
Примеры контейнеров проекта: `ElementsDrawHandlerContainer`, `GameButtonContainer`, `BoilCoffeeContainer`, `InterfaceScoreContainer`, `GameTimerContainer`, `RewardShopContainer`, `ShowCoffeeContainer`.

## Правила
- Не содержит доменной логики — только ссылки и простые методы доступа.
- Живёт в сцене и пересоздаётся при её перезагрузке, тогда как системы постоянны.
- Механизм подробно разобран в ноде «Контейнеры сцены» блока Паттерны.

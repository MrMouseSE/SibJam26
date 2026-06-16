---
tags: [фича, система, coffee, sibjam26]
type: note
---

# Система — SelectionButtonsSystem

↑ [[Элементы (Element System)]]

## Что это
Управляет двумя кнопками этапа выбора — Complete (завершить выбор) и Redraw (пересдать). В начале стейта выбора активирует обе кнопки с анимацией, а также хранит их контейнеры и настраивает длительности анимаций hover/click.

## Активные стейты
Работает только в стейте `SelectElements`:

```csharp
public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
{
    if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.SelectElements) return;
    Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
}
```

## Состав модуля
- `SelectionButtonsSystem` — система, делегирует работу механике.
- `SelectionButtonMechanic` — механика: привязка контейнеров кнопок и их активация.
- `SelectionButtonComponent` — данные: флаг `IsSelectionStateStartedThisFrame`, ссылки `CompleteButton` и `RedrawButton`.
- `GameButtonContainer` — MonoBehaviour кнопки: коллайдер, анимации активации/клика/ховера, тултип, событие `OnButtonPressed`, метод `SetActive`.
- `ButtonTooltipContainer` — MonoBehaviour подсказки кнопки: текст и анимация появления.
- `BoilButtonContainer` — наследник `GameButtonContainer` с дополнительным спрайтом.

## Как работает (по коду)
Контейнеры кнопок привязываются извне через `SetButtonsContainers`: тултипам форсится состояние, сохраняются ссылки, проставляются длительности hover/click из `GameSpeedSystem`:

```csharp
public void SetButtonsContainers(GameButtonContainer complete, GameButtonContainer redraw, GameSystemsHandler gameSystemsHandler)
{
    complete.ButtonTooltipContainer.AppearAnimation.SetForceState(true);
    redraw.ButtonTooltipContainer.AppearAnimation.SetForceState(true);
    Component.CompleteButton = complete;
    Component.RedrawButton = redraw;
    GameSpeedSystem speedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
    ...
}
```

Активация кнопок происходит один раз в начале стейта выбора — по флагу, который поднимает `ElementsDrawSystem`:

```csharp
public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
{
    if (!Component.IsSelectionStateStartedThisFrame) return;
    Component.IsSelectionStateStartedThisFrame = false;
    GameSpeedSystem speedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
    Component.CompleteButton.SetActive(true, speedSystem.Component.AnimationsDescription.ActivateAnimationDuration);
    Component.RedrawButton.SetActive(true, speedSystem.Component.AnimationsDescription.ActivateAnimationDuration);
}
```

`GameButtonContainer.SetActive` включает/выключает коллайдер, проигрывает звук и анимацию активации. Через события мыши (`OnMouseUp`, `OnMouseEnter`, `OnMouseExit`) кнопка проигрывает анимации клика/ховера и вызывает `OnButtonPressed`.

## Данные и зависимости
- `GameSpeedSystem` — `AnimationsDescription`: длительности активации, hover и click.
- Контейнеры `CompleteButton` и `RedrawButton` используются другими системами этапа: `CompleteSelectionSystem` гасит обе кнопки при завершении, `ElementsRedrawSystem` управляет кнопкой пересдачи.

---
tags: [фича, система, coffee, sibjam26]
type: note
---

# Система — CompleteBoilingSystem

↑ [[Кипячение (Boiling System)]]

## Что это
Система кнопки завершения кипячения. Хранит флаг `IsBoilingComplete`, реагирует на нажатие кнопки и при завершении прячет кнопку и переводит игру в стейт `CalculateValue`.

## Активные стейты
Без фильтра по стейту — `UpdateSystem` вызывает механику каждый кадр:

```csharp
public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
{
    Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
}
```

> [!note]
> Реальная работа ограничена ранним выходом `if (!Component.IsBoilingComplete) return;`, так что эффект наступает только после нажатия кнопки или перегрева в процессе.

## Состав модуля
- `CompleteBoilingSystem` (`IGameSystem`) — вызывает механику каждый кадр.
- `CompleteBoilingComponent` — `bool IsBoilingComplete`, `BoilButtonContainer CompleteBoilingButtonContainer`.
- `CompleteBoilingMechanic` (`IGameMechanic`) — привязка кнопки, тултип, обработка завершения.

## Как работает (по коду)
Привязка кнопки подписывается на событие нажатия и форсит исходные состояния анимаций:

```csharp
public void SetButtonContainer(BoilButtonContainer buttonContainer)
{
    buttonContainer.ButtonTooltipContainer.AppearAnimation.SetForceState(true);
    Component.CompleteBoilingButtonContainer = buttonContainer;
    Component.CompleteBoilingButtonContainer.OnButtonPressed += OnButtonClicked;
    Component.CompleteBoilingButtonContainer.ButtonActivateAnimations.SetForceState(true);
}
```

Нажатие кнопки только поднимает флаг:

```csharp
private void OnButtonClicked()
{
    Component.IsBoilingComplete = true;
}
```

В `UpdateMechanic` при поднятом флаге кнопка прячется и стейт меняется на расчёт:

```csharp
if (!Component.IsBoilingComplete) return;
Component.IsBoilingComplete = false;
GameSpeedSystem speedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
float duration = speedSystem.Component.AnimationsDescription.ClickAnimationsDuration;
Component.CompleteBoilingButtonContainer.SetActive(false, duration);
gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.CalculateValue);
```

`PlayTooltip` запускает анимацию тултипа кнопки через `UniTaskAnimationLazyObject` с длительностью `ButtonTooltipAnimationDuration`. `DisposeMechanic` отписывается от `OnButtonPressed`.

## Данные и зависимости
- `BoilButtonContainer` — `OnButtonPressed`, `ButtonTooltipContainer`, `ButtonActivateAnimations`, `ExtremeButtonSprite`, `SetActive(...)`.
- `GameSpeedSystem` — `ClickAnimationsDuration`, `ButtonTooltipAnimationDuration`.
- Флаг `IsBoilingComplete` совместно используется с `BoilingProcessSystem` (перегрев также поднимает его).
- Переключает стейт на `CalculateValue`.

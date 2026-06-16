---
tags: [фича, система, coffee, sibjam26]
type: note
---

# Система — StartBoilingSystem

↑ [[Кипячение (Boiling System)]]

## Что это
Стартовая система кипячения. Один раз на входе в стейт включает кнопку завершения, тултип, звук и анимации индикатора/процесса, перемещает камеру на фокус варки и сразу передаёт управление в стейт `BoilingCoffee`.

## Активные стейты
Работает только на `StartBoil`:

```csharp
if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.StartBoil) return;
Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
```

## Состав модуля
- `StartBoilingSystem` (`IGameSystem`) — фильтр по стейту, делегирует механике.
- `StartBoilingComponent` — пустой (состояние не хранится).
- `StartBoilingMechanic` (`IGameMechanic`) — вся логика запуска.

## Как работает (по коду)
В `UpdateMechanic` достаются соседние системы через `GetGameSystem`: `CompleteBoilingSystem`, `GameSpeedSystem`, `BoilingProcessSystem`, `GameCameraSystem`.

Включается кнопка завершения и проигрывается тултип:

```csharp
float duration = speedSystem.Component.AnimationsDescription.ClickAnimationsDuration;
completeBoilingSystem.Component.CompleteBoilingButtonContainer.SetActive(true, duration);
completeBoilingSystem.Mechanic.PlayTooltip(speedSystem);
```

Запускаются звук появления и анимации контейнера варки (`AnimateByUpdate = true`), затем камера двигается на точку фокуса, и стейт сразу переключается:

```csharp
var boilCoffeeContainer = processSystem.Component.Container;
boilCoffeeContainer.SoundContainer.Play(SoundType.AppearSound);
boilCoffeeContainer.IndicatorShakeTweenGroup.AnimateByUpdate = true;
boilCoffeeContainer.ProcessBoilTweenGroup.AnimateByUpdate = true;

cameraSystem.Mechanic.AnimateCameraMovement(boilCoffeeContainer.NormalCameraPoint, boilCoffeeContainer.BoildFocusCameraPoint,
    speedSystem.Component.AnimationsDescription.Camera);
gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.BoilingCoffee);
```

> [!note]
> Контейнер варки берётся не из своего компонента, а из `BoilingProcessSystem.Component.Container` — то есть система запуска фактически работает поверх данных процесса.

## Данные и зависимости
- `GameSpeedSystem` — длительности (`ClickAnimationsDuration`) и `AnimationsDescription.Camera`.
- `BoilingProcessSystem` — `BoilCoffeeContainer` (звук, твины, точки камеры).
- `CompleteBoilingSystem` — кнопка завершения и её тултип.
- `GameCameraSystem` — `AnimateCameraMovement`.
- Переключает стейт на `BoilingCoffee`.

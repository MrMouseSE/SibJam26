---
tags: [фича, система, coffee, sibjam26]
type: note
---

# Система — BoilingProcessSystem

↑ [[Кипячение (Boiling System)]]

## Что это
Сам процесс кипячения: каждый кадр накапливает `BoilValue` со случайным приращением, обновляет цвет/угол стрелки индикатора и прозрачность кнопки экстрима, проигрывает звук действия. При завершении (по кнопке или при перегреве) считает итоговый множитель `BoilMultiplier`.

## Активные стейты
Работает только на `BoilingCoffee`:

```csharp
if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.BoilingCoffee) return;
Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
```

## Состав модуля
- `BoilingProcessSystem(BoilDescription description)` (`IGameSystem`) — создаётся с описанием тайминга.
- `BoilingProcessComponent` — хранит `BoilingTime`, `BoilValue`, `BoilMultiplier`, `BoilDescription`, ссылку на `BoilCoffeeContainer`.
- `BoilingProcessMechanic` (`IGameMechanic`) — логика накопления и расчёта.
- `BoilCoffeeContainer` (`MonoBehaviour`) — сцена варки: спрайты индикатора, стрелка, твины, точки камеры, `SoundContainer`, `ProcessCancellationToken`.

## Как работает (по коду)
Привязка контейнера задаёт длительность твина процесса из описания:

```csharp
public void SetButtonContainer(BoilCoffeeContainer container)
{
    Component.Container = container;
    Component.Container.ProcessBoilTweenGroup.Duration = Component.BoilDescription.BoilProcessAnimationDuration;
}
```

Перегрев: если `BoilValue` превысил `BoilExtreemeValue`, кипячение помечается завершённым принудительно:

```csharp
if (Component.BoilValue > boilingDescription.BoilExtreemeValue)
{
    compeleBoilingSystem.Component.IsBoilingComplete = true;
    Component.BoilValue = 0f;
}
```

При завершении (флаг `IsBoilingComplete`): останавливаются анимации/звук, индикатор сбрасывается, камера возвращается в норму и по кривой считается множитель:

```csharp
container.ProcessCancellationToken.Cancel();
...
container.SoundContainer.Play(SoundType.DeathSound);
...
var normalizeBoilValue = Component.BoilValue/ boilingDescription.BoilExtreemeValue;
Component.BoilMultiplier = (boilingDescription.BoilMultiplyerCurve.Evaluate(normalizeBoilValue) + 1) * boilingDescription.MultValue;
Component.BoilValue = 0f;
return;
```

> [!note]
> Множитель считается из `BoilValue` уже после строки, которая в ветке перегрева обнуляет `BoilValue` ещё до расчёта. При нормальном завершении (по кнопке) значение сохраняется и `normalizeBoilValue` отражает накопленный прогресс.

Накопление за кадр со случайным шагом и звук действия:

```csharp
Component.BoilingTime += deltaTime;
Component.BoilValue += deltaTime * Random.Range(boilingDescription.BoilAddRangeMultiplier.x, boilingDescription.BoilAddRangeMultiplier.y);
SetIndicatorValues(boilingDescription, compeleBoilingSystem.Component.CompleteBoilingButtonContainer.ExtremeButtonSprite);
```

`SetIndicatorValues` лерпит цвет (`NormalColor`→`ExtreemeColor`), угол стрелки (`Vector3.zero`→`ArrowExtreemeAngle`) и альфу кнопки экстрима по `normalizeBoilValue = BoilValue / BoilExtreemeValue`.

## Данные и зависимости
- `BoilDescription` (`AnimationsDescription.Boil`, `ScriptableObject`): `BoilExtreemeValue`, `BoilAddRangeMultiplier`, `BoilMultiplyerCurve`, `MultValue`, `NormalColor`, `ExtreemeColor`, `ArrowExtreemeAngle`, `BoilProcessAnimationDuration`.
- `CompleteBoilingSystem` — флаг `IsBoilingComplete` и кнопка с `ExtremeButtonSprite`.
- `GameSpeedSystem` — `AnimationsDescription.Boil` и `Camera`.
- `GameCameraSystem` — возврат камеры при завершении.
- `BoilCoffeeContainer` — все визуальные/звуковые элементы и `ProcessCancellationToken`.
- Результат `BoilMultiplier` — вход для расчёта результата (`CalculateValue`).

---
tags: [фича, система, coffee, sibjam26]
type: note
---

# Система — ElementsDrawSystem

↑ [[Элементы (Element System)]]

## Что это
Раздаёт и отрисовывает «руку» элементов: настраивает контейнеры элементов по вместимости руки, проигрывает анимацию появления и после её завершения переводит игру к стейту выбора.

## Активные стейты
Работает только в стейте `DrawElements`:

```csharp
public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
{
    if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.DrawElements) return;
    Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
}
```

В ходе работы переводит игру в `AwaitAnimation`, а по завершении анимации появления — в `SelectElements`.

## Состав модуля
- `ElementsDrawSystem` — система, в `Initialize` передаёт механике ссылку на `GameSystemsHandler`.
- `ElementsDrawMechanic` — механика отрисовки и анимации.
- `ElementsDrawComponent` — данные: ссылка на `ElementsDrawHandlerContainer` (`Container`) и список фактически розданных элементов `DrawedElements`.

## Как работает (по коду)
Количество элементов к раздаче берётся из вместимости руки. Контейнеры перебираются: первые `currentElementToDrawCount` помечаются используемыми, остальным выключаются коллайдер и видимость:

```csharp
int currentElementToDrawCount = handSystem.Component.CurrentHandSlotCapacity;

Component.DrawedElements.Clear();
for (var index = 0; index < Component.Container.ElementContainers.Count; index++)
{
    var container = Component.Container.ElementContainers[index];
    bool isUsed = index < currentElementToDrawCount;
    container.ElementSelectAnimation.SetForceState(true);
    container.ElementDisappearAnimation.SetForceState(isUsed);
    var color = container.BackSpriteRenderer.color;
    color.a = isUsed ? 1f : 0f;
    container.BackSpriteRenderer.color = color;
    container.IsSelected = false;
    container.IsUsedInGame = isUsed;
    container.ElementCollider.enabled = isUsed;
    container.AnimationsDescription = speedSystem.Component.AnimationsDescription;
    StaticElementFactory.SetValuesToContainer(container);
    if (isUsed)
        Component.DrawedElements.Add(container);
}
```

Затем запускается анимация появления контейнера-руки, игра переводится в `AwaitAnimation`, а кнопкам выбора выставляется флаг старта стейта:

```csharp
Component.Container.AppearAnimation.SetForceState(true);

CancellationTokenSource cts = new CancellationTokenSource();
UniTaskAnimationLazyObject animObj = new (Component.Container.AppearAnimation, ref cts,
    speedSystem.Component.AnimationsDescription.ElementsAnimationDescription.ElementDrawAnimationDuration, true);
animObj.Play().Forget();
animObj.AnimationCompleted += OnDrawAnimationFinished;

selectionButtonSystem.Component.IsSelectionStateStartedThisFrame = true;

gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.AwaitAnimation);
```

По завершении анимации переходит к выбору:

```csharp
public void OnDrawAnimationFinished(UniTaskAnimationLazyObject animationObject)
{
    animationObject.AnimationCompleted -= OnDrawAnimationFinished;
    _gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.SelectElements);
}
```

## Данные и зависимости
- `ElementsDrawHandlerContainer` — контейнер руки: список `ElementContainers` и `AppearAnimation`.
- `StaticElementFactory` — заполняет значения элемента в контейнере (`SetValuesToContainer`).
- `ElementsHandSystem` — вместимость руки (число раздаваемых элементов).
- `GameSpeedSystem` — `AnimationsDescription` (длительности анимаций).
- `SelectionButtonsSystem` — поднимает флаг `IsSelectionStateStartedThisFrame` для активации кнопок.

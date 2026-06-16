---
tags: [фича, система, coffee, sibjam26]
type: note
---

# Система — CompleteSelectionSystem

↑ [[Элементы (Element System)]]

## Что это
Завершает этап выбора по нажатию кнопки Complete: проверяет, что в рецепт что-то добавлено, гасит кнопки выбора, анимирует исчезновение использованных элементов руки и переводит игровой цикл к сравнению рецепта.

## Активные стейты
Работает только в стейте `SelectElements`:

```csharp
public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
{
    if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.SelectElements) return;
    Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
}
```

При завершении переводит игру в `AwaitAnimation`, а по окончании анимации кнопки — в `CompareRecipe`.

## Состав модуля
- `CompleteSelectionSystem` — система, в `Initialize` передаёт механике `GameSystemsHandler`.
- `CompleteSelectionMechanic` — механика завершения выбора и смены стейта.
- `CompleteSelectionComponent` — данные: флаг `IsButtonPressedThisFrame`, ссылка на `CompleteButtonContainer`.

## Как работает (по коду)
Кнопка привязывается через `SetButtonContainer`, система подписывается на нажатие; обработчик поднимает флаг:

```csharp
Component.CompleteButtonContainer.OnButtonPressed += OnSelectionComplete;
...
private void OnSelectionComplete()
{
    Component.IsButtonPressedThisFrame = true;
}
```

Если в рецепте нет ни одного элемента — показывается тултип, и завершение отменяется:

```csharp
if (fillSystem.Component.RecipeContainers.Count < 1)
{
    var tooltipContainer = Component.CompleteButtonContainer.ButtonTooltipContainer;
    UniTaskAnimationLazyObject animTooltipObj = new(tooltipContainer.AppearAnimation, ref tooltipContainer.CancelToken,
        speedSystem.Component.AnimationsDescription.ButtonTooltipAnimationDuration,true);
    animTooltipObj.Play().Forget();
    Component.IsButtonPressedThisFrame = false;
    return;
}
```

Иначе сбрасывается счётчик пересдач, гасятся обе кнопки выбора и запускается анимация кнопки Complete:

```csharp
handSystem.Component.UnusedSwaps = 0;
var selectionSystem = (SelectionButtonsSystem)gameSystemsHandler.GetGameSystem(typeof(SelectionButtonsSystem));
selectionSystem.Component.CompleteButton.SetActive(false, speedSystem.Component.AnimationsDescription.ActivateAnimationDuration);
selectionSystem.Component.RedrawButton.SetActive(false, speedSystem.Component.AnimationsDescription.ActivateAnimationDuration);

var cancelToken = new CancellationTokenSource();
var animationObject = new UniTaskAnimationLazyObject(Component.CompleteButtonContainer.ButtonActivateAnimations,
    ref cancelToken, speedSystem.Component.AnimationsDescription.ActivateAnimationDuration, false);
animationObject.Play().Forget();
animationObject.AnimationCompleted += OnCompleteButtonAnimationFinished;
```

Все использованные элементы руки исчезают (с разной длительностью для выбранных/невыбранных), затем игра переходит в `AwaitAnimation`:

```csharp
foreach (var container in drawSystem.Component.Container.ElementContainers)
{
    float duration = container.IsSelected
        ? speedSystem.Component.AnimationsDescription.ElementsAnimationDescription.ElementSelectedDisappearDuration :
        speedSystem.Component.AnimationsDescription.ElementsAnimationDescription.ElementUnselectedDisappearDuration;

    if (container.IsUsedInGame)
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        UniTaskAnimationLazyObject animObj = new UniTaskAnimationLazyObject(container.ElementDisappearAnimation, ref cts, duration, true);
        animObj.Play().Forget();
        container.SoundContainer.Play(SoundType.DeathSound);
    }

    container.ElementCollider.enabled = false;
    container.IsUsedInGame = false;
}
_gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.AwaitAnimation);
```

По завершении анимации кнопки выполняется переход к сравнению рецепта:

```csharp
private void OnCompleteButtonAnimationFinished(UniTaskAnimationLazyObject uniTaskAnimationObject)
{
    uniTaskAnimationObject.AnimationCompleted -= OnCompleteButtonAnimationFinished;
    _gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.CompareRecipe);
}
```

## Данные и зависимости
- `FillRecipeSystem` — список `RecipeContainers` (проверка непустого рецепта).
- `ElementsHandSystem` — сброс счётчика `UnusedSwaps`.
- `SelectionButtonsSystem` — контейнеры `CompleteButton` и `RedrawButton` для гашения.
- `ElementsDrawSystem` — список `ElementContainers` для анимации исчезновения.
- `GameSpeedSystem` — длительности анимаций из `AnimationsDescription`.
- `GameButtonContainer` (`CompleteButtonContainer`) — кнопка завершения с событием `OnButtonPressed`, анимациями и тултипом.

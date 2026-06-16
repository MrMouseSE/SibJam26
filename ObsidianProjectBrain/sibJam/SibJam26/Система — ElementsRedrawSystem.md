---
tags: [фича, система, coffee, sibjam26]
type: note
---

# Система — ElementsRedrawSystem

↑ [[Элементы (Element System)]]

## Что это
Реализует пересдачу (redraw): по нажатию кнопки Redraw снимает выделение со всех элементов, добавленных в рецепт, перезаполняет их значения и расходует пересдачу. При исчерпании лимита пересдач кнопка отключается.

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
- `ElementsRedrawSystem` — система, делегирует работу механике.
- `ElementsRedrawMechanic` — механика пересдачи и подписка на нажатие кнопки.
- `ElementsRedrawComponent` — данные: флаг `IsRedrawButtonPressed`, ссылка на `RedrawButtonContainer`.

## Как работает (по коду)
Контейнер кнопки привязывается через `SetButtonContainer`, где система подписывается на нажатие; обработчик лишь поднимает флаг:

```csharp
Component.RedrawButtonContainer.OnButtonPressed += RedrawButtonPushed;
...
private void RedrawButtonPushed()
{
    Component.IsRedrawButtonPressed = true;
}
```

В апдейте пересдача выполняется только если в рецепте есть элементы. Расходуется пересдача (`UnusedSwaps`), и при достижении лимита кнопка скрывается с показом тултипа:

```csharp
if (selectionSystem.Component.RecipeContainers.Count == 0) return;

Component.IsRedrawButtonPressed = false;

handSystem.Component.UnusedSwaps ++;
if (handSystem.Component.UnusedSwaps == handSystem.Component.CurrentSwapCount)
{
    var tooltipContainer = Component.RedrawButtonContainer.ButtonTooltipContainer;
    UniTaskAnimationLazyObject animTooltipObj = new(tooltipContainer.AppearAnimation, ref tooltipContainer.CancelToken,
        speedSystem.Component.AnimationsDescription.ButtonTooltipAnimationDuration,true);
    animTooltipObj.Play().Forget();
    Component.RedrawButtonContainer.SetActive(false,
        speedSystem.Component.AnimationsDescription.ActivateAnimationDuration);
}
```

Затем для каждого элемента рецепта перезаполняются значения, снимается выделение и проигрывается анимация, после чего рецепт очищается:

```csharp
foreach (var recipeContainer in selectionSystem.Component.RecipeContainers)
{
    StaticElementFactory.SetValuesToContainer(recipeContainer);
    float duration =
        speedSystem.Component.AnimationsDescription.ElementsAnimationDescription.ElementSelectionAnimationDuration;

    UniTaskAnimationLazyObject animObj = new(recipeContainer.ElementSelectAnimation, ref recipeContainer.CancelToken,
        duration, !recipeContainer.IsSelected);

    animObj.Play().Forget();
    recipeContainer.IsSelected = false;
}
selectionSystem.Component.RecipeContainers.Clear();
```

> [!note]
> Здесь `selectionSystem` — это `FillRecipeSystem` (система заполнения рецепта), а не `SelectionButtonsSystem`; имя переменной в коде вводит в заблуждение. Пересдача не меняет стейт игры.

## Данные и зависимости
- `ElementsHandSystem` — счётчики `UnusedSwaps` и лимит `CurrentSwapCount`.
- `FillRecipeSystem` — список `RecipeContainers` (элементы, добавленные в рецепт).
- `GameSpeedSystem` — длительности анимаций из `AnimationsDescription`.
- `StaticElementFactory` — перезаполнение значений контейнера.
- `GameButtonContainer` (`RedrawButtonContainer`) — кнопка пересдачи с событием `OnButtonPressed` и тултипом.

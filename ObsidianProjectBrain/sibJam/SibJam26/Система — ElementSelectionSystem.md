---
tags: [фича, система, coffee, sibjam26]
type: note
---

# Система — ElementSelectionSystem

↑ [[Элементы (Element System)]]

## Что это
Обрабатывает клики игрока по элементам руки: при попадании курсора в коллайдер элемента переключает его состояние «выбран / не выбран» и проигрывает соответствующую анимацию.

## Активные стейты
Работает только в стейте `SelectElements`:

```csharp
public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
{
    if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.SelectElements) return;
    var inputSystem = (GameInputSystem)gameSystemsHandler.GetGameSystem(typeof(GameInputSystem));
    if (!inputSystem.Component.MouseWasPressedThisFrame) return;
    if (inputSystem.Component.MousePressedHitColliders.Count == 0) return;
    ...
}
```

## Состав модуля
- `ElementSelectionSystem` — система, читает ввод и определяет затронутый элемент.
- `ElementSelectionMechanic` — механика переключения выделения и анимации.
- `ElementSelectionComponent` — данные: `CurrentTouchedContainer` (текущий нажатый элемент).

## Как работает (по коду)
Система достаёт первый задетый коллайдер из ввода и получает с него `ElementContainer`, после чего вызывает механику:

```csharp
SelectionMechanic.Component.CurrentTouchedContainer =
    inputSystem.Component.MousePressedHitColliders[0].GetComponent<ElementContainer>();
SelectionMechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
```

Механика проигрывает звук и анимацию выделения, инвертируя флаг `IsSelected`:

```csharp
container.SoundContainer.Play(SoundType.ActionSound);

var gameSpeedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
float duration = gameSpeedSystem.Component.AnimationsDescription.ElementsAnimationDescription.ElementSelectionAnimationDuration;
UniTaskAnimationLazyObject animObj = new(container.ElementSelectAnimation,
    ref container.CancelToken, duration, !container.IsSelected);

animObj.Play().Forget();
container.IsSelected = !container.IsSelected;
```

> [!note]
> Сама система не добавляет элемент в рецепт — она только переключает визуальное выделение контейнера. Наполнение рецепта (`RecipeContainers`) выполняет система заполнения рецепта `FillRecipeSystem`.

## Данные и зависимости
- `GameInputSystem` — флаг `MouseWasPressedThisFrame` и список `MousePressedHitColliders`.
- `ElementContainer` — поля `ElementSelectAnimation`, `CancelToken`, `IsSelected`, `SoundContainer`.
- `GameSpeedSystem` — длительность анимации выделения из `AnimationsDescription`.

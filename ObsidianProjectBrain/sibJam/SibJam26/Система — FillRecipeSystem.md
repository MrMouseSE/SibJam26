---
tags: [фича, система, coffee, sibjam26]
type: note
---

# Система — FillRecipeSystem

↑ [[Рецепт (Recipe System)]]

## Что это
Накапливает контейнеры элементов, которые игрок выбирает (тапает) на стейте выбора. Работает как toggle: повторный тап по элементу убирает его из рецепта.

## Активные стейты
`SelectElements`. Дополнительно требует факт нажатия мыши в текущем кадре.

## Состав модуля
- `FillRecipeSystem` — `IGameSystem`, фильтрует стейт и ввод.
- `FillRecipeComponent` — список `RecipeContainers` (`List<ElementContainer>`).
- `FillRecipeMechanic` — `IGameMechanic`, логика toggle add/remove.

## Как работает (по коду)
Система пропускает кадр, если стейт не `SelectElements` или мышь не была нажата:

```csharp
public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
{
    if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.SelectElements) return;
    var inputSystem = (GameInputSystem)gameSystemsHandler.GetGameSystem(typeof(GameInputSystem));
    if (!inputSystem.Component.MouseWasPressedThisFrame) return;
    Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
}
```

Механика берёт текущий «тронутый» контейнер из `ElementSelectionSystem` и переключает его наличие в рецепте:

```csharp
var elementSystem = (ElementSelectionSystem)gameSystemsHandler.GetGameSystem(typeof(ElementSelectionSystem));
if (elementSystem.SelectionComponent.CurrentTouchedContainer == null) return;
if (Component.RecipeContainers.Contains(elementSystem.SelectionComponent.CurrentTouchedContainer))
{
    Component.RecipeContainers.Remove(elementSystem.SelectionComponent.CurrentTouchedContainer);
}
else
{
    Component.RecipeContainers.Add(elementSystem.SelectionComponent.CurrentTouchedContainer);
}
```

## Данные и зависимости
- Данные: `RecipeContainers` (`List<ElementContainer>`) — собираемый рецепт.
- Зависимости: `GameInputSystem` (флаг `MouseWasPressedThisFrame`), `ElementSelectionSystem` (`CurrentTouchedContainer`), `StateSystem`.
- Потребители списка: `CompareRecipeSystem` (сравнение) и `ApproveRecipeSystem` (списание и очистка).

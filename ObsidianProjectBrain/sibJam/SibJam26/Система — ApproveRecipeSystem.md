---
tags: [фича, система, coffee, sibjam26]
type: note
---

# Система — ApproveRecipeSystem

↑ [[Рецепт (Recipe System)]]

## Что это
Фиксирует выбранный рецепт: списывает использованные элементы из инвентаря, очищает накопленный список рецепта и передаёт управление показу кофе.

## Активные стейты
`ApproveRecipe`. По завершении переводит игру в `CoffeeShow`.

> [!note]
> Списание элементов необратимо — `RecipeContainers` очищается в этом же кадре после удаления из инвентаря.

## Состав модуля
- `ApproveRecipeSystem` — `IGameSystem`, фильтрует стейт.
- `ApproveRecipeComponent` — пустой (состояния не хранит).
- `ApproveRecipeMechanic` — `IGameMechanic`, списание и смена стейта.

## Как работает (по коду)
Механика проходит по всем контейнерам собранного рецепта, удаляет каждый элемент из статического инвентаря, очищает список и меняет стейт:

```csharp
var fillRecipeSystem = (FillRecipeSystem)gameSystemsHandler.GetGameSystem(typeof(FillRecipeSystem));

foreach (var recipeContainer in fillRecipeSystem.Component.RecipeContainers)
{
    ElementsStaticInventory.RemoveElementInInventory(recipeContainer.ElementName);
}

fillRecipeSystem.Component.RecipeContainers.Clear();
gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.CoffeeShow);
```

## Данные и зависимости
- Данные: собственных нет (`ApproveRecipeComponent` пуст).
- Зависимости: `FillRecipeSystem` (`RecipeContainers`), `ElementsStaticInventory` (статическое списание `RemoveElementInInventory`), `StateSystem`.
- Эффект: очистка `RecipeContainers` влияет на последующие проходы кластера.

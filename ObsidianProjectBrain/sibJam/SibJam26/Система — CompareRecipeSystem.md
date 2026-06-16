---
tags: [фича, система, coffee, sibjam26]
type: note
---

# Система — CompareRecipeSystem

↑ [[Рецепт (Recipe System)]]

## Что это
Сравнивает собранный игроком набор элементов с эталонными рецептами уровня и выбирает результирующий рецепт. Если совпадений нет — берётся дефолтный рецепт, иначе — совпавший с наибольшим множителем.

## Активные стейты
`CompareRecipe`. По завершении переводит игру в `StartBoil`.

> [!note]
> Переход `CompareRecipe → StartBoil` происходит сразу в `UpdateMechanic`, без ожидания анимации.

## Состав модуля
- `CompareRecipeSystem` — `IGameSystem`, фильтрует стейт.
- `CompareRecipeComponent` — поле `CurrentRecipe` (`CoffeeRecipeDescription`).
- `CompareRecipeMechanic` — `IGameMechanic`, отбор рецепта и смена стейта.

## Как работает (по коду)
Механика берёт рецепты из `RecipeHandlerSystem` и собранные контейнеры из `FillRecipeSystem`, фильтрует через `CompareWithRecipe`:

```csharp
var recipeHandlerSystem = (RecipeHandlerSystem)gameSystemsHandler.GetGameSystem(typeof(RecipeHandlerSystem));
var fillRecipeSystem = (FillRecipeSystem)gameSystemsHandler.GetGameSystem(typeof(FillRecipeSystem));
List<CoffeeRecipeDescription> matchedRecipes =
    recipeHandlerSystem.Component.CurrentCoffeeDescription.Recipes
        .Where(recipe => recipe.CompareWithRecipe(fillRecipeSystem.Component.RecipeContainers)).ToList();
Component.CurrentRecipe = matchedRecipes.Count > 0 ?
    matchedRecipes.OrderByDescending(x=>x.RecipeMultiplier).ToList()[0] : recipeHandlerSystem.Component.CurrentCoffeeDescription.DefaultRecipe;

gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.StartBoil);
```

Сам матчинг живёт в `CoffeeRecipeDescription.CompareWithRecipe`: проверяется совпадение по именам элементов и по типам (типы вычёркиваются из копии списка):

```csharp
public bool CompareWithRecipe(List<ElementContainer> elements)
{
    int elementsMatch = 0;
    foreach (var elementDescription in ElementDescriptions)
        if (elements.Any(x => x.ElementName == elementDescription.ElementName))
            elementsMatch++;
    bool isEelementsMatch = elementsMatch == ElementDescriptions.Count;

    List<ElementType> elementTypesRef = elements.Select(container => container.ElementType).ToList();
    bool isTypesMatch = true;
    foreach (var unused in ElementTypes.Where(elementType => !elementTypesRef.Remove(elementType)))
        isTypesMatch = false;
    return isEelementsMatch && isTypesMatch;
}
```

## Данные и зависимости
- Данные: `CurrentRecipe` — итоговый выбранный `CoffeeRecipeDescription`.
- Зависимости: `RecipeHandlerSystem` (`Recipes`, `DefaultRecipe`), `FillRecipeSystem` (`RecipeContainers`), `StateSystem`.
- Потребитель `CurrentRecipe`: `ShowCoffeeSystem` (спрайт и название кофе).

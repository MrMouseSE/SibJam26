---
tags: [фича, система, coffee, sibjam26]
type: note
---

# Система — RecipeHandlerSystem

↑ [[Рецепт (Recipe System)]]

## Что это
Пассивное хранилище текущего описания кофе уровня. Держит ссылку на `CoffeeDescription`, из которого другие системы кластера берут эталонные рецепты (`Recipes`, `DefaultRecipe`).

## Активные стейты
Нет. `UpdateSystem` и `UpdateMechanic` пусты — система не реагирует на стейты, только хранит данные.

## Состав модуля
- `RecipeHandlerSystem` — `IGameSystem`, конструируется с `CoffeeDescription`.
- `RecipeHandlerComponent` — поле `CurrentCoffeeDescription`.
- `RecipeHandlerMechanic` — `IGameMechanic`, тело пустое.

## Как работает (по коду)
Конструктор принимает `CoffeeDescription` и складывает его в компонент:

```csharp
public RecipeHandlerSystem(CoffeeDescription description)
{
    Component = new RecipeHandlerComponent(description);
    Mechanic = new RecipeHandlerMechanic(Component);
}
```

```csharp
public class RecipeHandlerComponent
{
    public CoffeeDescription CurrentCoffeeDescription;

    public RecipeHandlerComponent(CoffeeDescription coffeeDescription)
    {
        CurrentCoffeeDescription = coffeeDescription;
    }
}
```

`Initialize`, `UpdateSystem`, `DisposeSystem` пусты. Доступ к данным — напрямую через `Component.CurrentCoffeeDescription`.

## Данные и зависимости
- Данные: `CoffeeDescription` (ScriptableObject) — поля `Recipes`, `DefaultRecipe`, элементы уровня.
- Потребители: `CompareRecipeSystem` читает `Component.CurrentCoffeeDescription.Recipes` и `DefaultRecipe`.
- Внешних зависимостей при работе нет (данные передаются в конструкторе).

---
tags: [фича, coffee, sibjam26]
type: hub
---

# 📜 Рецепт (Recipe System)

↑ [[Фичи]]

Кластер `CoffeeSystemsScripts/RecipeSystemScripts`.

## Назначение
Хранит целевой рецепт уровня, накапливает выбранные игроком элементы, сравнивает собранный набор с эталонными рецептами `CoffeeDescription`, подтверждает выбор (списывает элементы из инвентаря) и показывает итоговый кофе с анимированными счётчиками результата.

## Активные стейты
Системы кластера работают на стейтах `SelectElements` (наполнение), `CompareRecipe` (сравнение), `ApproveRecipe` (подтверждение) и `CoffeeShow` (показ). `RecipeHandlerSystem` стейтов не проверяет — это пассивное хранилище.

## Системы
- [[Система — RecipeHandlerSystem]] — хранит текущий `CoffeeDescription` уровня.
- [[Система — FillRecipeSystem]] — наполняет рецепт выбранными элементами на стейте `SelectElements`.
- [[Система — CompareRecipeSystem]] — сравнивает собранный рецепт с эталонами на стейте `CompareRecipe`.
- [[Система — ApproveRecipeSystem]] — подтверждает рецепт и списывает элементы на стейте `ApproveRecipe`.
- [[Система — ShowCoffeeSystem]] — показывает готовый кофе и значения результата на стейте `CoffeeShow`.

## Данные
- `CoffeeDescription` (ScriptableObject) — список рецептов `Recipes`, дефолтный рецепт `DefaultRecipe`, элементы уровня.
- `CoffeeRecipeDescription` (ScriptableObject) — эталонный рецепт: набор элементов/типов, стоимость `RecipeCost`, множитель `RecipeMultiplier`, метод `CompareWithRecipe`.

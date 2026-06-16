---
tags: [архитектура, стейт-машина, sibjam26]
type: note
---

# 🔁 Стейт-машина GameStates

↑ [[Архитектура]]

`enum GameStates` (`Assets/Scripts/GameScripts/GameStates.cs`). Глобальное состояние держит `GameStateSystem` (поля `CurrentGameState` / `PreviousGameState` в `GameStateComponent`). Каждая система выполняет логику только в «своих» стейтах; переход = смена `CurrentGameState`, которую инициирует механика-владелец этапа.

## Цикл уровня
```
StartGame → DrawElements → SelectElements → CompareRecipe → StartBoil
→ BoilingCoffee → CalculateValue → ApproveRecipe → CoffeeShow
→ CompareLevelComplete → RewardElements → ChangeLevel → UpdateLevelView ─┐
        ▲                                                                │
        └──────────────────── (новый уровень) ───────────────────────────┘
```
`UpdateLevelView` замыкает цикл обратно в `DrawElements`. `AwaitAnimation` — промежуточный «гейт» во время анимаций. `GameOver` фактически не выставляется системами фич (см. ниже).

## Таблица переходов (по коду систем)
| State | Владелец перехода | Куда переводит |
|---|---|---|
| StartGame | `StartGameSystem` | DrawElements |
| DrawElements | системы отрисовки руки | (AwaitAnimation) → SelectElements |
| SelectElements | `CompleteSelectionSystem` / `FillRecipeSystem` | (AwaitAnimation) → CompareRecipe |
| CompareRecipe | `CompareRecipeSystem` | StartBoil |
| StartBoil | `StartBoilingSystem` | BoilingCoffee |
| BoilingCoffee | `CompleteBoilingSystem` | CalculateValue |
| CalculateValue | `CalculateResultSystem` | ApproveRecipe |
| ApproveRecipe | `ApproveRecipeSystem` | CoffeeShow |
| CoffeeShow | `ShowCoffeeSystem` (по завершении анимации) | CompareLevelComplete |
| CompareLevelComplete | `LevelCompleteSystem` | RewardElements (победа) / ChangeLevel (поражение) |
| RewardElements | `RewardElementsSystem` | ChangeLevel |
| ChangeLevel | `LevelHandlerSystem` | UpdateLevelView |
| UpdateLevelView | `LevelViewSystem` | DrawElements (замыкает цикл) |
| AwaitAnimation | системы анимаций | следующий целевой стейт |
| GameOver | — | не выставляется (см. note) |

> [!note] `GameCompleteSystem` проверяет флаг `IsGameComplete` каждый кадр, но логика конца игры — заглушка (`//TODO`); ни одна система фич не переводит в `GameOver`. Подробности — в ноде `Система — GameCompleteSystem`.

> [!note] Часть переходов идёт не мгновенно, а через `AwaitAnimation` и колбэки анимаций (например `CoffeeShow → CompareLevelComplete`). Точная механика — в нодах соответствующих систем блока Фичи.

## Как расширять
1. Добавить элемент в `GameStates`, определить место в последовательности.
2. Зафиксировать переход: кто переводит, при каком условии, в какой следующий стейт.
3. Обновить системы, активные в новом стейте.
4. Проверить отсутствие тупиков и неуправляемых циклов.
5. Обновить эту ноду и таблицу.

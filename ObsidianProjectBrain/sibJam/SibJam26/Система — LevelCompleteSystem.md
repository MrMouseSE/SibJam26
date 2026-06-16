---
tags: [фича, система, sibjam26]
type: note
---

# Система — LevelCompleteSystem

↑ [[Уровни (Levels)]]

## Что это
Система проверки, пройден ли текущий уровень. Сравнивает итоговый результат раунда с целевым счётом текущего уровня и решает, выдавать награду или переигрывать.

## Активные стейты
Работает только на стейте `CompareLevelComplete`:

```csharp
public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
{
    if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.CompareLevelComplete) return;
    Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
}
```

Дальнейшие переходы: при успехе → `RewardElements`, при провале → `ChangeLevel`.

## Состав модуля
- `LevelCompleteSystem : IGameSystem` — точка входа, привязка к стейту.
- `LevelCompleteComponent` — состояние: флаг `IsLevelCompleted` и ссылка на `DaysAchievementsDescription DaysDescription`.
- `LevelCompleteMechanic : IGameMechanic` — собственно логика сравнения.

## Как работает (по коду)
Механика берёт результат раунда из `CalculateResultSystem` и текущие индексы дня/уровня из `LevelHandlerSystem`, затем сравнивает результат с порогом `LevelScoreToAchieve` цели:

```csharp
var resultSystem = (CalculateResultSystem)gameSystemsHandler.GetGameSystem(typeof(CalculateResultSystem));
var levelHandlerComponent = ((LevelHandlerSystem)gameSystemsHandler.GetGameSystem(typeof(LevelHandlerSystem))).Component;
Component.IsLevelCompleted = false;

if (resultSystem.Component.ResultValue >
    Component.DaysDescription.DaysAchievementsDescriptions[levelHandlerComponent.Day].
        LevelsAchievements[levelHandlerComponent.Level].LevelScoreToAchieve)
{
    Component.IsLevelCompleted = true;
    gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.RewardElements);
    return;
}

gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.ChangeLevel);
```

- Флаг `IsLevelCompleted` сбрасывается в `false` в начале и поднимается только при превышении порога.
- При победе выставляется флаг и переход на `RewardElements` (выдача награды).
- При провале — сразу переход на `ChangeLevel` (там обрабатывается сброс прогресса). В коде помечено `//TODO: not complete restart levels`.

## Данные и зависимости
- `DaysAchievementsDescription` (ScriptableObject) — список дней, у каждого список `LevelAchievementDescription` с полем `LevelScoreToAchieve`.
- `CalculateResultSystem` — источник `ResultValue` (через `GetGameSystem`).
- `LevelHandlerSystem` — источник индексов `Day` и `Level`.
- `StateSystem` — переключение стейта через `Mechanic.ChangeState`.

---
tags: [фича, система, sibjam26]
type: note
---

# Система — LevelHandlerSystem

↑ [[Уровни (Levels)]]

## Что это
Система переключения прогресса: хранит текущие индексы дня и уровня и продвигает их вперёд (или сбрасывает при проигрыше). Также настраивает звуки победы/поражения.

## Активные стейты
Работает только на стейте `ChangeLevel`:

```csharp
public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
{
    if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.ChangeLevel) return;
    Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
}
```

По завершении всегда переходит на `UpdateLevelView`.

## Состав модуля
- `LevelHandlerSystem : IGameSystem` — точка входа, привязка к стейту.
- `LevelHandlerComponent` — состояние: `int Level`, `int Day`, `SoundContainer AudioContainer`, `DaysAchievementsDescription DaysDescription`; метод `GetLevelAchievementDescription()` возвращает текущую цель.
- `LevelHandlerMechanic : IGameMechanic` — логика продвижения и звуки.

## Как работает (по коду)
Механика читает флаг `IsLevelCompleted` у `LevelCompleteSystem` и в зависимости от него двигает прогресс:

```csharp
var levelCompleteSystem = (LevelCompleteSystem)gameSystemsHandler.GetGameSystem(typeof(LevelCompleteSystem));
if (levelCompleteSystem.Component.IsLevelCompleted)
{
    if (Component.Level == Component.DaysDescription.DaysAchievementsDescriptions[Component.Day].LevelsAchievements.Count)
    {
        Component.Level = 0;
        if (Component.Day < Component.DaysDescription.DaysAchievementsDescriptions.Count)
        {
            Component.Day++;
        }
        else
        {
            var completeSystem = (GameCompleteSystem)gameSystemsHandler.GetGameSystem(typeof(GameCompleteSystem));
            completeSystem.Component.IsGameComplete = true;
        }
    }
    else
    {
        Component.AudioContainer.Play(SoundType.AppearSound);
        Component.Level++;
    }
}
else
{
    Component.AudioContainer.Play(SoundType.DeathSound);
    Component.Level = 0;
    Component.Day = 0;
}

gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.UpdateLevelView);
```

- Победа, уровень не последний → играет звук появления (`AppearSound`) и `Level++`.
- Победа, уровни дня закончились → сброс `Level = 0` и переход на следующий день (`Day++`); если дни тоже исчерпаны — выставляется `GameCompleteComponent.IsGameComplete = true`.
- Проигрыш → звук поражения (`DeathSound`) и полный сброс прогресса (`Level = 0`, `Day = 0`).
- В любом случае в конце переход на `UpdateLevelView`.

Настройка звуков выполняется отдельным методом `SetSoundContainer`:

```csharp
public void SetSoundContainer(SoundContainer soundContainer)
{
    Component.AudioContainer = soundContainer;
    Component.AudioContainer.AppearClips = new SoundPair[1] { Component.DaysDescription.WinClip };
    Component.AudioContainer.DeathClips = new SoundPair[1] { Component.DaysDescription.LoseClip };
}
```

> [!note]
> Сравнение `Level == LevelsAchievements.Count` и условие `Day < ...Count` используют `Count` как границу — это потенциальный off-by-one (`//TODO`-зоны в фиче), документируется как есть по коду.

## Данные и зависимости
- `DaysAchievementsDescription` — структура дней/уровней, плюс `WinClip` и `LoseClip`.
- `SoundContainer` (`SoundsComponentsScripts`) — воспроизведение `AppearSound`/`DeathSound`.
- `LevelCompleteSystem` — источник флага `IsLevelCompleted`.
- `GameCompleteSystem` — установка флага завершения игры.
- `StateSystem` — переход на `UpdateLevelView`.

---
tags: [фича, система, ui, sibjam26]
type: note
---

# Система — LevelViewSystem

↑ [[Очки и таймер (ScoreView)]]

## Что это
Индикатор дня и уровня в HUD. Обновляет тексты номера дня/уровня и инициирует старт отрисовки элементов нового уровня.

## Активные стейты
Работает **только** в стейте `UpdateLevelView` — в остальных `UpdateSystem` делает ранний `return`:

```csharp
public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
{
    if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.UpdateLevelView) return;
    Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
}
```

## Состав модуля
- `LevelViewSystem` — реализует `IGameSystem`, создаёт `Component` и `Mechanic`.
- `LevelViewMechanic` — логика обновления индикатора и проброс требуемых очков.
- `LevelViewComponent` — единственное поле `LevelViewContainer Container`.
- `LevelViewContainer` (MonoBehaviour) — `TMP_Text DayIndex`, `TMP_Text LevelIndex`.

## Как работает (по коду)
`SetViewContainer` привязывает контейнер и ставит стартовые значения `"1"`/`"1"`:

```csharp
public void SetViewContainer(LevelViewContainer container)
{
    Component.Container = container;
    Component.Container.DayIndex.text = "1";
    Component.Container.LevelIndex.text = "1";
}
```

`UpdateMechanic` (в стейте `UpdateLevelView`) читает текущий день/уровень из `LevelHandlerSystem`, обновляет тексты, переключает игру в `DrawElements` и обновляет требуемые очки:

```csharp
public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
{
    var levelSystem = (LevelHandlerSystem)gameSystemsHandler.GetGameSystem(typeof(LevelHandlerSystem));
    var day = levelSystem.Component.DaysDescription.DaysAchievementsDescriptions[levelSystem.Component.Day];
    Component.Container.DayIndex.text = day.DayIndex.ToString();
    var level = day.LevelsAchievements[levelSystem.Component.Level];
    Component.Container.LevelIndex.text = level.Level.ToString();
    gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.DrawElements);
    ShowRequiredScore(gameSystemsHandler);
}
```

`ShowRequiredScore` делегирует обновление требуемого счёта системе очков:

```csharp
public void ShowRequiredScore(GameSystemsHandler gameSystemsHandler)
{
    var scoreSystem = (InterfaceScoreSystem)gameSystemsHandler.GetGameSystem(typeof(InterfaceScoreSystem));
    scoreSystem.Mechanic.ShowRequiredScore(gameSystemsHandler);
}
```

> [!note]
> Система — точка перехода `UpdateLevelView → DrawElements`: после обновления индикатора она сама дёргает `ChangeState(DrawElements)`, поэтому в `UpdateLevelView` отрабатывает однократно за вход в стейт.

## Данные и зависимости
- `LevelHandlerSystem` — `Component.Day`, `Component.Level`, `DaysDescription.DaysAchievementsDescriptions[...]` (поля `DayIndex`, `LevelsAchievements[].Level`).
- `InterfaceScoreSystem` — вызов `Mechanic.ShowRequiredScore` для обновления требуемых очков.
- `gameSystemsHandler.StateSystem.Mechanic.ChangeState` — смена стейта на `DrawElements`.

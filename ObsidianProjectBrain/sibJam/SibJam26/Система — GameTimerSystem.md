---
tags: [фича, система, ui, sibjam26]
type: note
---

# Система — GameTimerSystem

↑ [[Очки и таймер (ScoreView)]]

## Что это
Игровой таймер: накапливает прошедшее время уровня и отображает его в формате `MM:SS`.

## Активные стейты
Каждый кадр во всех стейтах, кроме `StartGame` (ранний `return`):

```csharp
public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
{
    if (gameSystemsHandler.StateSystem.Component.CurrentGameState == GameStates.StartGame) return;
    Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
}
```

## Состав модуля
- `GameTimerSystem` — реализует `IGameSystem`, создаёт `Component` и `Mechanic`.
- `GameTimerMechanic` — отсчёт времени и форматирование строки.
- `GameTimerComponent` — `GameTimerContainer Container`, `float ElapsedTime`.
- `GameTimerContainer` (MonoBehaviour) — единственное поле `TMP_Text TimerText`.

## Как работает (по коду)
`SetContainer` привязывает контейнер и сбрасывает таймер в ноль, затем сразу рисует:

```csharp
public void SetContainer(GameTimerContainer container)
{
    Component.Container = container;
    Component.ElapsedTime = 0f;
    if (Component.Container == null || Component.Container.TimerText == null) return;
    UpdateView();
}
```

`UpdateMechanic` каждый кадр прибавляет `deltaTime` и перерисовывает (с защитой от null-контейнера):

```csharp
public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
{
    if (Component.Container == null || Component.Container.TimerText == null) return;
    Component.ElapsedTime += deltaTime;
    UpdateView();
}
```

`UpdateView` форматирует накопленные секунды как минуты:секунды:

```csharp
private void UpdateView()
{
    var timeSpan = TimeSpan.FromSeconds(Component.ElapsedTime);
    Component.Container.TimerText.text = $"{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
}
```

> [!note]
> Используются `timeSpan.Minutes`/`timeSpan.Seconds` (компоненты), а не `TotalMinutes`. После 60 минут отображение сбросится на `00`.

## Данные и зависимости
- `System.TimeSpan` — форматирование времени.
- `deltaTime` из цикла обновления систем — единственный источник приращения.
- Внешних систем (`GetGameSystem`) не использует.

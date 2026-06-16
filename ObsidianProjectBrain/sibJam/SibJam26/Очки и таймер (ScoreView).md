---
tags: [фича, ui, sibjam26]
type: hub
---

# 🏆 Очки и таймер (ScoreView)

↑ [[Фичи]]

Кластер `GameSystemsScripts/ScoreViewScripts`. Под-хаб фичи игрового HUD.

## Назначение
Отображение игрового HUD во время варки кофе: требуемые/текущие/максимальные очки и набор доступных элементов (`InterfaceScoreSystem`), игровой таймер прохождения (`GameTimerSystem`), индикатор номера дня и уровня (`LevelViewSystem`).

## Активные стейты
- `InterfaceScoreSystem` и `GameTimerSystem` работают каждый кадр во **всех** стейтах, кроме `StartGame` (ранний `return` при `CurrentGameState == StartGame`).
- `LevelViewSystem` работает только в стейте `UpdateLevelView`.

## Системы
- [[Система — InterfaceScoreSystem]] — отрисовка очков (требуемые/предыдущие/максимальные) и панели доступных элементов.
- [[Система — GameTimerSystem]] — отсчёт и форматирование игрового времени `MM:SS`.
- [[Система — LevelViewSystem]] — индикатор дня/уровня, инициирует переход `UpdateLevelView → DrawElements`.

> [!note]
> `InterfaceScoreSystem` и `LevelViewSystem` связаны: `LevelViewMechanic.ShowRequiredScore` дёргает `InterfaceScoreSystem.Mechanic.ShowRequiredScore`, чтобы обновить требуемые очки при смене уровня.

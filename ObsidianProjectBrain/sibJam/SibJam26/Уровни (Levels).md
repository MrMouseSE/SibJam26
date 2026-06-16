---
tags: [фича, sibjam26]
type: hub
---

# 📈 Уровни (Levels)

↑ [[Фичи]]

Кластер `GameSystemsScripts/LevelsSystemScripts`.

## Назначение
Прогресс игрока по структуре «дни → уровни». Фича отвечает за три шага в конце раунда: проверку, пройден ли уровень (сравнение результата с целевым счётом), переключение текущего уровня/дня и финальную проверку завершения всей игры.

Данные о целях задаются ScriptableObject `DaysAchievementsDescription` (список `DayAchievementsDescription`, каждый со своим списком `LevelAchievementDescription`).

## Активные стейты
- `CompareLevelComplete` — работает `LevelCompleteSystem`.
- `ChangeLevel` — работает `LevelHandlerSystem`.
- `GameCompleteSystem` работает каждый кадр (без привязки к стейту) и реагирует на флаг `IsGameComplete`.

Переходы по коду: `CompareLevelComplete` → (`RewardElements` при победе или `ChangeLevel` при проигрыше) → `ChangeLevel` → `UpdateLevelView`.

> [!note]
> `GameOver` в коде систем этой фичи напрямую не выставляется — конец игры обрабатывается через флаг `GameCompleteComponent.IsGameComplete` (логика-заглушка с `//TODO`).

## Системы
- [[Система — LevelCompleteSystem]] — проверка условия прохождения уровня на стейте `CompareLevelComplete`.
- [[Система — LevelHandlerSystem]] — смена уровня/дня на стейте `ChangeLevel`.
- [[Система — GameCompleteSystem]] — финальная проверка завершения игры по флагу.

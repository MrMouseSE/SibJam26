---
tags: [приём, sibjam26]
type: note
---

# 🎚️ State-driven системы

↑ [[Приёмы]]

## Суть
Каждая система в `UpdateSystem` сначала проверяет текущий `GameState` (через `GameStateSystem`) и выполняет логику только в «своих» стейтах. Если стейт не подходит — `UpdateSystem` завершается без вызова механики.

```csharp
public void UpdateSystem(GameSystemsHandler handler, float deltaTime)
{
    if (handler.StateSystem.CurrentState != GameStates.SelectElements) return;
    _mechanic.UpdateMechanic(handler, deltaTime);
}
```
(обобщённый вид; конкретная проверка зависит от системы)

## Зачем
- Предсказуемый порядок этапов игрового цикла.
- Минимум конфликтов логики между подсистемами — в каждый момент активна узкая группа систем.
- Переход между этапами = смена `CurrentState`, которую инициирует механика-владелец стейта.

## Зона ответственности
- `GameStateSystem` хранит и меняет `CurrentState`.
- Механики переключают стейт по завершении своего этапа (например, `CompleteSelectionSystem` → переход к `CompareRecipe`).

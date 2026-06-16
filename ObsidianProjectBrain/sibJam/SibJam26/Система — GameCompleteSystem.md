---
tags: [фича, система, sibjam26]
type: note
---

# Система — GameCompleteSystem

↑ [[Уровни (Levels)]]

## Что это
Система финальной проверки завершения игры. Реагирует на флаг `IsGameComplete`, который выставляется в `LevelHandlerSystem`, когда исчерпаны все дни и уровни.

## Активные стейты
К конкретному стейту не привязана — `UpdateSystem` вызывает механику каждый кадр:

```csharp
public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
{
    Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
}
```

## Состав модуля
- `GameCompleteSystem : IGameSystem` — точка входа, создаёт компонент и механику.
- `GameCompleteComponent` — единственное поле `bool IsGameComplete`.
- `GameCompleteMechanic : IGameMechanic` — проверка флага.

## Как работает (по коду)
Механика каждый кадр проверяет флаг и при `true` должна выполнить завершение игры (рестарт и т.п.) — пока это заглушка:

```csharp
public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
{
    if (Component.IsGameComplete != true) return;


    //TODO: GAME COMPLETE HERE (restart or something)
}
```

> [!note]
> Реальная логика конца игры ещё не реализована (`//TODO: GAME COMPLETE HERE`). Флаг лишь поднимается в `LevelHandlerSystem`; стейт `GameOver` системой не выставляется.

## Данные и зависимости
- `GameCompleteComponent.IsGameComplete` — входной флаг, устанавливается извне (`LevelHandlerSystem`).
- Прямых зависимостей от других систем в текущем коде нет.

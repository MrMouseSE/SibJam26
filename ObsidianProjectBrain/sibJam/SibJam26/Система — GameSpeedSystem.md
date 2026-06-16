---
tags: [инфраструктура, sibjam26]
type: note
---

# Система — GameSpeedSystem

↑ [[Инфраструктура проекта]]

## Что это

`GameSpeedSystem` (`GameSystemsScripts/GameSpeedScripts`) — игровая система (`IGameSystem`), хранящая текущий темп игры и скорость анимаций. На данный момент это контейнер данных: механика и `UpdateSystem` пустые.

## Как работает (по коду)

В конструкторе принимается `AnimationsDescription` и пробрасывается в компонент:

```csharp
public GameSpeedSystem(AnimationsDescription animationsDescription)
{
    Component = new GameSpeedComponent(animationsDescription);
    Mechanic = new GameSpeedMechanic(Component);
}
```

`UpdateSystem` и `GameSpeedMechanic.UpdateMechanic` пустые — система ничего не делает в цикле, а служит хранилищем значений `CurrentGameSpeed` и `CurrentAnimationsSpeed` и ссылкой на описание анимаций для других систем.

## Данные и зависимости

- `AnimationsDescription` (`AnimationDescriptionsScripts`) — описание анимаций, передаётся в конструктор.
- `GameSpeedComponent` — поля `CurrentGameSpeed`, `CurrentAnimationsSpeed`, `AnimationsDescription`.

## Активные стейты

Система не проверяет `GameStates` — `UpdateSystem` пуст, гейтов по стейту нет.

## Состав модуля

- `GameSpeedSystem` — система, конструктор от `AnimationsDescription`.
- `GameSpeedMechanic` — пустая механика (заготовка).
- `GameSpeedComponent` — хранилище темпа игры и скорости анимаций.

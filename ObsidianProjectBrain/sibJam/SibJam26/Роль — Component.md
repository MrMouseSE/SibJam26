---
tags: [паттерн, роль, sibjam26]
type: note
---

# 📦 Роль — Component

↑ [[Паттерн System-Mechanic-Component-Container]]

## Что это
Слой данных фичи. Хранит состояние, по которому работают система и механика. Без интерфейса — обычный C#-класс с полями.

## Что делает
- Держит runtime-флаги, кэш, ссылки на внутренние объекты фичи.
- Является «общей памятью» между `System` и `Mechanic` (механика обычно получает компонент в конструкторе).

## Как работает (пример `GameStateComponent`)
```csharp
public class GameStateComponent
{
    public GameStates CurrentGameState;
    public GameStates PreviousGameState;
}
```
Система создаёт компонент и передаёт его механике:
```csharp
Component = new GameStateComponent();
Mechanic = new GameStateMechanic(Component);
```

## Правила
- Максимально «данные», без сложной бизнес-логики.
- Может держать ссылку на `Container` (как `ElementsDrawComponent.Container`), но сам не управляет Unity-объектами.
- Флаги-триггеры (`IsBoilingComplete`, `IsCoffeeShowing`, `IsCameraUpdating` и т.п.) живут именно здесь.

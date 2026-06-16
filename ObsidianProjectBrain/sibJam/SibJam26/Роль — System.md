---
tags: [паттерн, роль, sibjam26]
type: note
---

# 🧠 Роль — System

↑ [[Паттерн System-Mechanic-Component-Container]]

## Что это
Оркестрационный слой фичи. Решает, **когда** должна выполняться логика, и делегирует выполнение механике.

## Контракт
`IGameSystem` (`Assets/Scripts/GameSystemsScripts/IGameSystem.cs`):
```csharp
public interface IGameSystem
{
    void Initialize(GameSystemsHandler gameSystemsHandler);
    void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime);
    void DisposeSystem();
}
```

## Что делает
- В конструкторе создаёт свои `Component` и `Mechanic`.
- `Initialize` — стартовая подготовка после регистрации.
- `UpdateSystem` — вызывается каждый кадр из общего цикла; проверяет `GameState` (или флаг) и при выполнении условия дёргает `Mechanic.UpdateMechanic`.
- `DisposeSystem` — освобождение на уровне системы.

## Как работает (пример `GameStateSystem`)
```csharp
public GameStateSystem()
{
    Component = new GameStateComponent();
    Mechanic = new GameStateMechanic(Component);
}
public void Initialize(GameSystemsHandler h) => Mechanic.SetStartState();
public void UpdateSystem(GameSystemsHandler h, float dt) => Mechanic.UpdateMechanic(h, dt);
public void DisposeSystem() { }
```

## Правила
- Не содержит тяжёлой доменной логики.
- Не хранит Unity-ссылки напрямую (они приходят в механику/компонент через контейнер).
- Доступ к другим системам — только через `handler.GetGameSystem(...)`.

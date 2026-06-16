---
tags: [архитектура, слой, sibjam26]
type: note
---

# 🎛️ Слой — Runtime Orchestration

↑ [[Слои проекта]]

## Что это
Слой выполнения: держит все системы и крутит общий цикл обновления. Реализован классом `GameSystemsHandler` (`Assets/Scripts/ScenesOperatingScripts/GameSystemsHandler.cs`) — обычный C#-класс, не `MonoBehaviour`.

## Что делает
- Хранит реестр систем `Dictionary<Type, IGameSystem> GameSystems` (ключ — тип системы).
- Даёт доступ к стейт-системе через поле `GameStateSystem StateSystem`.
- Вызывает `Initialize`, `UpdateSystem`, `DisposeSystem` у всех систем.
- Является «шиной»: любая система достаёт другую через `GetGameSystem(typeof(...))`.

## Контракты систем
- `IGameSystem`: `Initialize(handler)`, `UpdateSystem(handler, deltaTime)`, `DisposeSystem()`.
- Механики реализуют `IGameMechanic`: `UpdateMechanic(handler, deltaTime)`, `DisposeMechanic()`.

## Как работает
Регистрация и доступ:
```csharp
public void AddGameSystem(IGameSystem gameSystem)
    => GameSystems.Add(gameSystem.GetType(), gameSystem);

public IGameSystem GetGameSystem(Type type) => GameSystems[type];

public void AddStateSystem(GameStateSystem stateSystem)
{
    StateSystem = stateSystem;       // прямой доступ к стейту
    AddGameSystem(stateSystem);
}
```

Цикл (запускается прямо в конструкторе):
```csharp
public GameSystemsHandler()
{
    _isProcess = true;
    UpdateSystems(_updateCancellationTokenSource.Token).Forget();
}

private async UniTask UpdateSystems(CancellationToken token)
{
    while (_isProcess)
    {
        foreach (var gameSystem in GameSystems)
            gameSystem.Value.UpdateSystem(this, Time.deltaTime);
        await UniTask.Yield();
    }
}
```

Завершение: `DisposeSystems()` ставит `_isProcess = false`, отменяет `CancellationTokenSource`, вызывает `DisposeSystem` у всех.

## Роль в архитектуре
Единый владелец жизненного цикла систем и точка их взаимодействия. Системы не знают друг о друге напрямую — только через `handler`.

## Риски
- Обход `GameSystems` идёт прямо в цикле: мутация коллекции (`Add`/`Remove`) во время обхода может бросить исключение.
- `GetGameSystem` бросит `KeyNotFoundException`, если система не зарегистрирована.

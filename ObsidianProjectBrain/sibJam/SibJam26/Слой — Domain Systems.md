---
tags: [архитектура, слой, sibjam26]
type: note
---

# ☕ Слой — Domain Systems

↑ [[Слои проекта]]

## Что это
Прикладной слой — игровые системы, реализующие геймплей и мету. Все лежат в `Assets/Scripts/GameSystemsScripts`, сгруппированы по доменным кластерам, и каждая построена по паттерну проекта (System / Mechanic / Component / Container).

## Что делает
Содержит всю игровую логику: набор элементов, рецепт, кипячение, расчёт результата, бустеры, награды, прогресс уровней, HUD, меню, экономику и инфраструктурные сервисы.

## Доменные кластеры
- `CoffeeSystemsScripts/` — ядро геймплея:
  - `ElementSystemScripts` (рука, отрисовка, выбор, redraw, завершение),
  - `RecipeSystemScripts` (handler, fill, compare, approve, show),
  - `BoilingSystemScripts` (start / process / complete),
  - `CalculateResultSystemScripts`,
  - `BoostersSystemScripts`,
  - `RewardElementsSystemScripts`.
- `LevelsSystemScripts/` — `LevelComplete`, `LevelHandler`, `GameComplete`.
- `MainMenuSystems/` — `StartGame`, `ExitGame`.
- `ScoreViewScripts/` — `InterfaceScore`, `GameTimer`, `LevelView`.
- Инфраструктура — `CameraSystem`, `GameInputScripts`, `GameStateScripts`, `GameSpeedScripts`, `EconomyScripts`.

## Как работает
- Каждая система регистрируется в `GameSystemsHandler` на этапе Bootstrap.
- Каждый кадр получает `UpdateSystem`, но выполняет логику только в «своих» игровых состояниях (state-driven).
- Unity-ссылки приходят не в систему напрямую, а в её механику/компонент через контейнер сцены.

## Типовая система (пример `GameStateSystem`)
```csharp
public class GameStateSystem : IGameSystem
{
    public GameStateComponent Component;
    public GameStateMechanic Mechanic;

    public GameStateSystem()
    {
        Component = new GameStateComponent();
        Mechanic = new GameStateMechanic(Component);
    }

    public void Initialize(GameSystemsHandler h) => Mechanic.SetStartState();
    public void UpdateSystem(GameSystemsHandler h, float dt) => Mechanic.UpdateMechanic(h, dt);
    public void DisposeSystem() { }
}
```
Видно каноничную связку: система создаёт `Component` и `Mechanic`, в `UpdateSystem` делегирует в механику.

## Роль в архитектуре
Здесь живёт «смысл игры». Слой полностью отделён от того, как объекты создаются (Bootstrap/фабрики), как обновляются (Orchestration) и где находятся на сцене (Scene Layer).

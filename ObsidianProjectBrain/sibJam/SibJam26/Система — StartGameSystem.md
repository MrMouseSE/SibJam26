---
tags: [фича, система, ui, sibjam26]
type: note
---

# Система — StartGameSystem

↑ [[Главное меню]]

## Что это
Система запуска игры из главного меню. Реагирует на нажатие кнопки Start, активирует игровую сцену, настраивает камеру и переводит игру в первый рабочий стейт игрового цикла.

Класс: `StartGameSystem` (`GameSystemsScripts/MainMenuSystems/StartGameSystemScripts`), реализует `IGameSystem`.

## Активные стейты
Логика срабатывает только в стейте `StartGame`. По итогу работы система переключает стейт на `DrawElements` (старт игрового цикла).

## Состав модуля
- `StartGameSystem` — оркестратор: создаёт компонент и механику, проверяет условия в `UpdateSystem`.
- `StartGameComponent` — данные: ссылка на `MenuButtonContainer ButtonContainer`, флаги `IsActionLock`, `IsStartGameButtonPushed`.
- `StartGameMechanic` — поведение: подписка на нажатие кнопки и переход в игровую сцену.
- `MenuButtonContainer` (`MainMenuScripts`) — внешний UI-контейнер кнопки, источник события `OnButtonPushed`.

## Как работает (по коду)
1. Конструктор `StartGameSystem(MenuButtonContainer container)` создаёт `StartGameComponent` из переданного контейнера кнопки и `StartGameMechanic` над ним.
2. Механика в конструкторе подписывается на событие кнопки и поднимает флаг при нажатии:
```csharp
Component.ButtonContainer.OnButtonPushed += StartGameButtonPushed;
// ...
private void StartGameButtonPushed()
{
    Component.IsStartGameButtonPushed = true;
}
```
3. `UpdateSystem` пропускает кадр, если действие заблокировано, текущий стейт не `StartGame`, либо кнопка ещё не нажата:
```csharp
if (Component.IsActionLock) return;
if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.StartGame) return;
if (Component.IsStartGameButtonPushed) Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
```
4. `UpdateMechanic` активирует игровую сцену, привязывает камеру к её `CameraHolder` и стартует игровой цикл сменой стейта:
```csharp
var root = SceneLoadingHandler.SetSceneActive(SceneNamesConst.GameScene);
var cameraSystem = gameSystemsHandler.GetGameSystem((typeof(CameraSystem.GameCameraSystem))) as CameraSystem.GameCameraSystem;
cameraSystem.Component.CurrentCameraHolder = root.GetCameraHolder();
cameraSystem.Component.IsCameraUpdating = true;
gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.DrawElements);
```
5. `DisposeSystem` → `DisposeMechanic` отписывается от события кнопки.

> [!note]
> Система запрашивает `GameCameraSystem` через `gameSystemsHandler.GetGameSystem(...)` — связь между системами не статическая, а через хэндлер во время выполнения.

## Данные и зависимости
- Вход: `MenuButtonContainer` (`StartButtonContainer`) — передаётся из `MainMenuSystemContainer`.
- Зависит от: `GameSystemsHandler`, `StateSystem` (чтение `CurrentGameState`, вызов `ChangeState`), `GameCameraSystem`, `SceneLoadingHandler` + `SceneNamesConst.GameScene`.
- Эффект: активирует `GameScene`, включает обновление камеры, переводит стейт `StartGame` → `DrawElements`.

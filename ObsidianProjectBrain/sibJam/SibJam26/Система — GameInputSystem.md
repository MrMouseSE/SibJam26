---
tags: [инфраструктура, sibjam26]
type: note
---

# Система — GameInputSystem

↑ [[Инфраструктура проекта]]

## Что это

`GameInputSystem` (`GameSystemsScripts/GameInputScripts`) — игровая система (`IGameSystem`), которая опрашивает мышь через Unity Input System и складывает результат (нажатие, позиция, попавшие коллайдеры) в компонент, откуда его читают другие системы.

## Как работает (по коду)

Каждый кадр (если ввод не заблокирован) механика читает состояние мыши. При нажатии левой кнопки берётся позиция и выполняется рейкаст по всем коллайдерам через камеру, полученную у `GameCameraSystem`:

```csharp
Component.MouseWasPressedThisFrame = Mouse.current.leftButton.wasPressedThisFrame;
if (Component.MouseWasPressedThisFrame)
{
    Component.MousePressedPosition = Mouse.current.position.ReadValue();
    var camera = ((GameCameraSystem)gameSystemsHandler.GetGameSystem(typeof(GameCameraSystem)))
        .Component.CurrentCamera.CameraObject;
    Component.MousePressedHitColliders = StaticSupportMethods.GetAllCollidersFromMouseCast(camera,
            Component.MousePressedPosition);
}
```

Аналогичный блок для `rightButton.wasPressedThisFrame` пишет в `MouseWasReleasedThisFrame`/`MouseReleasedPosition` (рейкаст там также через `MousePressedPosition`). Сами коллайдеры собирает `StaticSupportMethods.GetAllCollidersFromMouseCast` из `SupportScripts`.

## Данные и зависимости

- `UnityEngine.InputSystem` — источник состояния мыши (`Mouse.current`).
- `GameCameraSystem` — берётся через `gameSystemsHandler.GetGameSystem`, нужна `CameraObject` для рейкаста (см. [[Система — GameCameraSystem]]).
- `StaticSupportMethods` (`SupportScripts`) — рейкаст по коллайдерам.
- `GameInputComponent` — выходные данные: `IsInputLocked`, флаги нажатия/отпускания, позиции, списки `Collider`.

## Активные стейты

Система не проверяет `GameStates`. Обновление гейтится флагом `Component.IsInputLocked`:

```csharp
public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
{
    if (Component.IsInputLocked) return;
    Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
}
```

## Состав модуля

- `GameInputSystem` — система, конструктор без аргументов.
- `GameInputMechanic` — опрос мыши и рейкасты за кадр.
- `GameInputComponent` — флаги и буферы ввода (`MouseWasPressedThisFrame`, `MousePressedPosition`, `MousePressedHitColliders` и т.д.).

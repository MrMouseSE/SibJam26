---
tags: [инфраструктура, sibjam26]
type: note
---

# Система — GameCameraSystem

↑ [[Инфраструктура проекта]]

## Что это

`GameCameraSystem` (`GameSystemsScripts/CameraSystem`) — игровая система (`IGameSystem`), которая держит текущую камеру синхронизированной с холдером активной сцены и проигрывает анимированные перелёты камеры. Опирается на MonoBehaviour-объекты из `CameraScripts`: `CameraContainer` (сама камера) и `CameraHolder` (точка крепления + твин перелёта).

## Как работает (по коду)

В конструкторе создаются компонент и механика, камера передаётся снаружи:

```csharp
public GameCameraSystem(CameraContainer container)
{
    Component = new GameCameraComponent(container);
    Mechanic = new GameCameraMechanic(Component);
}
```

Каждый кадр (если включено слежение) механика жёстко копирует позицию/поворот холдера на камеру:

```csharp
public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
{
    Component.CurrentCamera.CameraTransform.position = Component.CurrentCameraHolder.CameraRoot.position;
    Component.CurrentCamera.CameraTransform.rotation = Component.CurrentCameraHolder.CameraRoot.rotation;
}
```

Перелёт камеры (`AnimateCameraMovement`) настраивает `TweenGroupAnimation` холдера: находит внутри группы `PositionTweenAnimation` и `RotateTweenAnimation`, проставляет from/to из переданных `Transform`, длительность и кривую из `CameraAnimationDescription`, затем запускает через `UniTaskAnimationLazyObject` (см. [[Подсистема — Твины]]), используя `CameraMoverCancellationToken` холдера для отмены предыдущего перелёта.

`CameraContainer` через `OnValidate` сам подтягивает `Camera` и `Transform`. `CameraHolder` хранит `CameraHandler`, `CameraRoot`, `CameraMoverTween` и токен отмены.

## Данные и зависимости

- `CameraContainer` — текущая камера (передаётся в конструктор).
- `CameraHolder` — холдер активной сцены (`CurrentCameraHolder`), задаётся извне.
- `CameraAnimationDescription` (`AnimationDescriptionsScripts`) — длительность и кривая перелёта.
- `TweenScripts` — `TweenGroupAnimation`, `PositionTweenAnimation`, `RotateTweenAnimation`, `UniTaskAnimationLazyObject`.

## Активные стейты

Система не проверяет `GameStates`. Слежение управляется флагом `Component.IsCameraUpdating`:

```csharp
public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
{
    if (!Component.IsCameraUpdating) return;
    Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
}
```

## Состав модуля

- `GameCameraSystem` — система, цикл `Initialize/UpdateSystem/DisposeSystem`.
- `GameCameraMechanic` — слежение за кадр + `AnimateCameraMovement`.
- `GameCameraComponent` — `IsCameraUpdating`, `CurrentCamera`, `CurrentCameraHolder`.
- `CameraContainer`, `CameraHolder` (`CameraScripts`) — MonoBehaviour-носители камеры и точки крепления.

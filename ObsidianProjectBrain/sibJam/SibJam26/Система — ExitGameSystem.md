---
tags: [фича, система, ui, sibjam26]
type: note
---

# Система — ExitGameSystem

↑ [[Главное меню]]

## Что это
Система выхода из приложения из главного меню. По нажатию кнопки Exit инициирует освобождение всех систем и закрывает приложение.

Класс: `ExitGameSystem` (`GameSystemsScripts/MainMenuSystems/ExitGameSystemScripts`), реализует `IGameSystem`.

## Активные стейты
Стейт `GameStates` не проверяется: система реагирует только на флаг нажатия кнопки и работает в любом стейте, пока активна сцена меню.

## Состав модуля
- `ExitGameSystem` — оркестратор: создаёт компонент и механику, в `UpdateSystem` запускает выход.
- `ExitGameComponent` — данные: ссылка на `MenuButtonContainer Container`, флаг `IsGameExitProcess`.
- `ExitGameMechanic` — поведение: подписка на кнопку и асинхронное закрытие приложения.
- `MenuButtonContainer` (`MainMenuScripts`) — внешний UI-контейнер кнопки, источник события `OnButtonPushed`.

## Как работает (по коду)
1. Конструктор `ExitGameSystem(MenuButtonContainer container)` создаёт `ExitGameComponent` и `ExitGameMechanic`.
2. Механика подписывается на нажатие кнопки и поднимает флаг процесса выхода:
```csharp
Component.Container.OnButtonPushed += ExitGameMethod;
// ...
private void ExitGameMethod()
{
    Component.IsGameExitProcess = true;
}
```
3. `UpdateSystem` при поднятом флаге освобождает все системы:
```csharp
if (Component.IsGameExitProcess)
{
    gameSystemsHandler.DisposeSystems();
}
```
4. `DisposeSystem` → `DisposeMechanic` отписывается от кнопки и запускает отложенное закрытие приложения:
```csharp
public void DisposeMechanic()
{
    Component.Container.OnButtonPushed -= ExitGameMethod;
    ExitApplication().Forget();
}

private async UniTaskVoid ExitApplication()
{
    await UniTask.WaitForSeconds(1f);
    #if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
    #else
    Application.Quit();
    #endif
}
```

> [!note]
> Само закрытие приложения происходит не сразу: `UpdateSystem` лишь вызывает `DisposeSystems()`, а реальный `Application.Quit()` / выход из Play Mode откладывается на 1 секунду внутри `DisposeMechanic` через `UniTask`.

## Данные и зависимости
- Вход: `MenuButtonContainer` (`ExitButtonContainer`) — передаётся из `MainMenuSystemContainer`.
- Зависит от: `GameSystemsHandler` (`DisposeSystems`), `Cysharp.Threading.Tasks` (UniTask), `UnityEngine.Application` / `UnityEditor.EditorApplication`.
- Эффект: освобождение всех систем и завершение приложения (в редакторе — выход из Play Mode).

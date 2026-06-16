---
tags: [фича, система, coffee, sibjam26]
type: note
---

# Система — ShowCoffeeSystem

↑ [[Рецепт (Recipe System)]]

## Что это
Показывает игроку готовый кофе: подставляет спрайт и название выбранного рецепта, запускает анимированные счётчики значений результата и проигрывает анимацию появления. По завершении анимации переводит игру к сравнению прогресса уровня.

## Активные стейты
`CoffeeShow`. По завершении анимации переводит игру в `CompareLevelComplete`.

> [!note]
> Стейт-переход `CoffeeShow → CompareLevelComplete` не мгновенный: он вызывается из колбэка `OnAnimationComplete` после двух фаз анимации (`OnShowEnd` → повтор → `OnAnimationComplete`). Флаг `IsCoffeeShowing` защищает от повторного запуска во время показа.

## Состав модуля
- `ShowCoffeeSystem` — `IGameSystem`, фильтрует стейт.
- `ShowCoffeeComponent` — флаг `IsCoffeeShowing`, ссылка на `Container`.
- `ShowCoffeeMechanic` — `IGameMechanic`, заполнение контейнера и управление анимацией.
- `ShowCoffeeContainer` — `MonoBehaviour` с UI/анимациями: спрайт кофе, тексты, счётчики `CountTweenAnimation`, аудио, токен отмены.

## Как работает (по коду)
Контейнер привязывается заранее через `SetContainer` (форсирует стартовое состояние анимации):

```csharp
public void SetContainer(ShowCoffeeContainer container, GameSystemsHandler gameSystemsHandler)
{
    container.CoffeShowAnimation.SetForceState(true);
    Component.Container = container;
    _gameSystemsHandler = gameSystemsHandler;
}
```

На стейте `CoffeeShow` механика берёт текущий рецепт из `CompareRecipeSystem` и результат из `CalculateResultSystem`, заполняет визуал и запускает анимацию:

```csharp
if (Component.IsCoffeeShowing) return;
Component.IsCoffeeShowing = true;
var speedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
var recipeSystem = (CompareRecipeSystem)gameSystemsHandler.GetGameSystem(typeof(CompareRecipeSystem));
var resultSystem = (CalculateResultSystem)gameSystemsHandler.GetGameSystem(typeof(CalculateResultSystem));
var result = resultSystem.Component;
var container = Component.Container;
var recipe = recipeSystem.Component.CurrentRecipe;
container.CoffeShowRenderer.sprite = recipe.CoffeeSprite;
container.CoffeNameText.text = recipe.RecipeName;

SetCounts(container, result);

container.CoffeShowAudioContainer.Play(SoundType.AppearSound);
UniTaskAnimationLazyObject animObj = new(container.CoffeShowAnimation, ref container.CancelToken,
    speedSystem.Component.AnimationsDescription.ShowCoffeeDuration, true);
animObj.AnimationCompleted += OnShowEnd;
animObj.Play().Forget();
```

`SetCounts` раздаёт целевые значения счётчикам (бодрость, вкус, их множители, стоимость рецепта, множитель кофе, множитель кипячения и итог `ResultValue`), включая режим `AnimateByUpdate`. Колбэки анимации:

```csharp
private void OnShowEnd(UniTaskAnimationLazyObject obj)
{
    obj.AnimationCompleted -= OnShowEnd;
    obj.AnimationCompleted += OnAnimationComplete;
    obj.Play(true).Forget();
}

private void OnAnimationComplete(UniTaskAnimationLazyObject obj)
{
    obj.AnimationCompleted -= OnAnimationComplete;
    _gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.CompareLevelComplete);
    Component.IsCoffeeShowing = false;
}
```

## Данные и зависимости
- Данные: `IsCoffeeShowing` (защита от повторного запуска), `Container` (`ShowCoffeeContainer`).
- Зависимости: `CompareRecipeSystem` (`CurrentRecipe` — спрайт и имя), `CalculateResultSystem` (`CalculateResultComponent` — значения счётчиков), `GameSpeedSystem` (`ShowCoffeeDuration`), `StateSystem`.
- Анимация/звук: `UniTaskAnimationLazyObject` (`TweenScripts`), `CountTweenAnimation`, `AudioContainer` (`SoundType.AppearSound`), `CancellationTokenSource`.
- Привязка контейнера выполняется извне через `SetContainer` до входа в стейт.

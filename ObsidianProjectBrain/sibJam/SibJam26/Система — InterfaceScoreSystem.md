---
tags: [фича, система, ui, sibjam26]
type: note
---

# Система — InterfaceScoreSystem

↑ [[Очки и таймер (ScoreView)]]

## Что это
Система игрового HUD, отвечающая за отображение очков (требуемых, предыдущих, максимальных) и панели текущих доступных элементов инвентаря во время варки.

## Активные стейты
Каждый кадр во всех стейтах, кроме `StartGame` — в нём `UpdateSystem` делает ранний `return`:

```csharp
public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
{
    if (gameSystemsHandler.StateSystem.Component.CurrentGameState == GameStates.StartGame) return;
    Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
}
```

Методы `SetContainer` / `ShowRequiredScore` / `UpdateScore` вызываются по событиям (привязка контейнера, смена уровня, расчёт результата) вне зависимости от стейта.

## Состав модуля
- `InterfaceScoreSystem` — реализует `IGameSystem`, создаёт `Component` и `Mechanic`.
- `InterfaceScoreMechanic` — вся логика отображения.
- `InterfaceScoreComponent` — хранит `Container` и кэш очков: `CurrentReachedScore`, `PreviousReachedScore`, `MaximumReachedScore`.
- `InterfaceScoreContainer` (MonoBehaviour) — три `ScoreContainer` (`RequireScore`, `PreviousScoreText`, `MaximumScoreText`) и `List<ElementCountPairContainer> CurrentElements`.
- `ScoreContainer` (MonoBehaviour) — `TMP_Text ScoreText`, `TweenAnimation ScoreAnimation`, `CountTweenAnimation ScoreCountAnimation`, поля `PreviousValue`/`CurrentValue`, `CancellationTokenSource CancToken`; метод `SetScoreToAnimation(value)` задаёт `FromCount`/`ToCount` анимации счётчика.
- `ElementCountPairContainer` (MonoBehaviour) — одна ячейка элемента: `CountText`, `ElementRenderer`, `BackRenderer`, `ElementSprite`, `Count`.

## Как работает (по коду)
Привязка контейнера и подтягивание сохранённых рекордов из `PlayerPrefs`:

```csharp
public void SetContainer(InterfaceScoreContainer interfaceScoreContainer, GameSystemsHandler gameSystemsHandler)
{
    Component.Container = interfaceScoreContainer;
    Component.Container.MaximumScoreText.ScoreText.text = PlayerPrefs.GetString("MaxScore");
    Component.Container.PreviousScoreText.ScoreText.text = PlayerPrefs.GetString("PreviousScore");
    ShowRequiredScore(gameSystemsHandler);
}
```

`ShowRequiredScore` берёт требуемый счёт текущего уровня из `LevelHandlerSystem` (день → уровень → `LevelScoreToAchieve`), длительность анимации из `GameSpeedSystem` и проигрывает анимацию числа:

```csharp
var score =
    levelHandler.Component.DaysDescription.DaysAchievementsDescriptions[levelHandler.Component.Day].
        LevelsAchievements[levelHandler.Component.Level].LevelScoreToAchieve;

var containerRequireScore = Component.Container.RequireScore;
containerRequireScore.SetScoreToAnimation(score);
UniTaskAnimationLazyObject animMaxObj = new(containerRequireScore.ScoreAnimation,
    ref containerRequireScore.CancToken, speedSystem.Component.AnimationsDescription.ScoreAnimationDuration, true);
animMaxObj.Play().Forget();
```

`UpdateScore(resultValue, duration)` — вызывается при подсчёте результата: обновляет максимум (если побит), сдвигает текущий счёт в предыдущий и анимирует оба текста через `UniTaskAnimationLazyObject`.

`UpdateMechanic` (каждый кадр) синхронизирует панель элементов с `ElementsStaticInventory.CurrentAvailableElements`, беря спрайты и цвет фона из `CurrentCoffeeDescription` (`RecipeHandlerSystem`):

```csharp
foreach (var element in ElementsStaticInventory.CurrentAvailableElements)
{
    var elementContainer = Component.Container.CurrentElements[index];
    elementContainer.ElementRenderer.enabled = element.Value > 0;
    var emenetDesc = coffeeDescription.Elements.Find(x=>x.ElementName == element.Key);
    elementContainer.ElementSprite = emenetDesc.Sprite;
    elementContainer.ElementRenderer.sprite = emenetDesc.Sprite;
    elementContainer.CountText.enabled = element.Value > 0;
    elementContainer.CountText.text = element.Value.ToString();
    elementContainer.BackRenderer.enabled = element.Value > 0;
    elementContainer.BackRenderer.color = emenetDesc.BackColor;
    index++;
}
```

## Данные и зависимости
- `LevelHandlerSystem` — текущий день/уровень и `LevelScoreToAchieve`.
- `GameSpeedSystem` — `AnimationsDescription.ScoreAnimationDuration`.
- `RecipeHandlerSystem` — `CurrentCoffeeDescription` (спрайты и цвета элементов).
- `ElementsStaticInventory.CurrentAvailableElements` — статический инвентарь доступных элементов.
- `PlayerPrefs` — ключи `MaxScore`, `PreviousScore`.
- Tween-слой: `UniTaskAnimationLazyObject`, `TweenAnimation`, `CountTweenAnimation`.

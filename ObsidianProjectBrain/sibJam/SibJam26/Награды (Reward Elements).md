---
tags: [фича, coffee, sibjam26]
type: note
---

# 🎁 Награды (Reward Elements)

↑ [[Фичи]]

Кластер `CoffeeSystemsScripts/RewardElementsSystemScripts`.

## Что это
Экран-магазин наград между уровнями: выдаёт бонусы за пройденный уровень (элементы, слоты руки, свопы, валюту) и предлагает три платных оффера — бустер, ингредиенты, дополнительный слот руки. Игрок нажимает «Continue», и игра уходит на смену уровня.

## Активные стейты
`UpdateMechanic` выполняется только на `RewardElements`:

```csharp
if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.RewardElements) return;
```

Выходы со стейта — всегда `ChangeLevel`:
- если уровень НЕ пройден — сразу `ChangeState(GameStates.ChangeLevel)`;
- после нажатия «Continue» (или если контейнер отсутствует) — `ExitRewardState` → `ChangeLevel`.

> [!note] Если `LevelCompleteSystem.Component.IsLevelCompleted == false`, экран наград пропускается целиком — состояние не инициализируется и бонусы не выдаются.

## Состав модуля
- `RewardElementsSystem` — `IGameSystem`. Конструктор принимает `DaysAchievementsDescription` и `RewardShopDescription`.
- `RewardElementsComponent` — конфиги (`DayAchievementsDescription`, `RewardShopDescription`), ссылка на `RewardShopContainer` и набор флагов: `IsRewardStateInitialized`, `IsContinuePressed`, `IsBooster/Ingredient/SlotPurchasedThisState`, `IsBooster/Ingredient/SlotBuyRequested`.
- `RewardElementsMechanic` — вся логика: вход/выход, обработка покупок, обновление UI.
- `RewardShopContainer : MonoBehaviour` — корень UI: текст валюты, четыре кнопки (`BoosterOfferButton`, `IngredientOfferButton`, `SlotOfferButton`, `ContinueButton`) и события `OnBuyBooster/OnBuyIngredients/OnBuySlot/OnContinue`.
- `RewardShopOfferButton : MonoBehaviour` — одна кнопка оффера: цена, текст состояния (`Buy`/`Locked`/`Purchased`), коллайдер, событие `OnPressed` (по `OnMouseUp`).
- `RewardShopDescription` — `ScriptableObject` (`CreateAssetMenu "Coffee/RewardShopDescription"`): `CurrencyRewardPerWin` и три оффера (`BoosterOffer`, `IngredientOffer`, `HandSlotOffer`).

## Как работает (по коду)
1. **Привязка контейнера** (`SetContainer`): отписывается от старого контейнера, привязывается к новому — `BindButtons()` и подписка на четыре события, затем прячет UI (`SetVisible(false)`).

2. **Вход в стейт** (`EnterRewardState`): однократно (флаг `IsRewardStateInitialized`) сбрасывает все флаги, затем выдаёт награды за достижение уровня:

```csharp
foreach (var bonus in levelAchievement.BonusElementsForAchieved)
    ElementsStaticInventory.AddElementToInventory(bonus.ElementName);
...
handSystem.Component.CurrentHandSlotCapacity += levelAchievement.AdditionalHandSlot;
handSystem.Component.CurrentSwapCount += levelAchievement.AdditionalSwap;
handSystem.Mechanic.FixHandCounts();
SaveHandValues(handSystem.Component);
```

Начисляет валюту за победу и сохраняет инвентарь:

```csharp
currencySystem.Mechanic.AddCurrency(Component.RewardShopDescription.CurrencyRewardPerWin);
ElementsStaticInventory.SaveCurrentAvailableElements();
```

Затем `RefreshUI` и `SetVisible(true)`.

3. **Покупки через флаги-запросы**: кнопки лишь ставят флаги (`RequestBoosterBuy` → `IsBoosterBuyRequested` и т.д.). На следующем апдейте `ProcessBuyRequests` сбрасывает флаг и вызывает соответствующий `Buy...Offer`. Так покупки выполняются в потоке игры, а не из колбэка UI.

4. **Покупка бустера** (`BuyBoosterOffer`): если уже куплен в этом стейте или нет оффера — выход. Если бустер уже куплен ранее — помечает и выходит. Иначе `TrySpend(offer.Price)`; если разблокировать не удалось — возвращает деньги:

```csharp
if (!currencySystem.Mechanic.TrySpend(offer.Price)) { RefreshUI(...); return; }
if (!boostersSystem.Mechanic.TryUnlockBoosterPermanent(offer.BoosterName))
{
    currencySystem.Mechanic.AddCurrency(offer.Price); // откат
    RefreshUI(...); return;
}
```

5. **Покупка ингредиентов** (`BuyIngredientOffer`): `TrySpend`, затем по каждому `IngredientRewardItem` добавляет в инвентарь `Count` штук элемента и сохраняет инвентарь.

6. **Покупка слота** (`BuySlotOffer`): `TrySpend`, увеличивает `CurrentHandSlotCapacity` на `offer.SlotIncrement`, `FixHandCounts()`, сохраняет.

7. **Обновление UI** (`RefreshUI`): пишет баланс в `CurrencyText`, по каждому офферу выставляет цену и состояние кнопки `SetState(canBuy, purchased)` — `canBuy` = `balance >= price`. Кнопка «Continue» всегда активна.

8. **Кнопка оффера** (`RewardShopOfferButton.SetState`): включает/выключает коллайдер (`canBuy && !purchased`) и текст состояния — `Purchased` / `Buy` / `Locked`.

9. **Выход** (`ExitRewardState`): прячет UI, сбрасывает `IsRewardStateInitialized`, переходит в `ChangeLevel`.

## Данные и зависимости
- **Конфиги:** `RewardShopDescription` (валюта за победу + офферы), `DaysAchievementsDescription`; награды уровня — `levelHandler.Component.GetLevelAchievementDescription()`.
- **Читает из:** `LevelCompleteSystem` (`IsLevelCompleted`), `LevelHandlerSystem` (награды уровня), `PlayerCurrencySystem` (баланс).
- **Пишет в:** `PlayerCurrencySystem` (начисление/трата), `BoostersSystem` (`TryUnlockBoosterPermanent`), `ElementsHandSystem` (слоты/свопы), `ElementsStaticInventory` (элементы).
- **Персистентность:** инвентарь и баланс сохраняются своими системами; слоты/свопы руки — `PlayerPrefs` (`ElementsHandCapacity`, `ElementsHandSwapCount`).
- **Связанные фичи:** `Бустеры (Boosters)`, `Экономика (Economy)`.

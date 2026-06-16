---
tags: [фича, coffee, sibjam26]
type: note
---

# ⚡ Бустеры (Boosters)

↑ [[Фичи]]

Кластер `CoffeeSystemsScripts/BoostersSystemScripts`.

## Что это
Система постоянных множителей результата, которые срабатывают, если в собранном рецепте присутствуют требуемые элементы. Бустеры описываются в `ScriptableObject`, могут покупаться навсегда (сохранение в `PlayerPrefs`) и каждый раунд пересчитывают итоговый множитель `CurrentMultiplier`, который потом читает расчёт результата.

## Активные стейты
`UpdateMechanic` выполняется только на `CalculateValue`:

```csharp
if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.CalculateValue) return;
```

Покупка/разблокировка бустеров (`TryUnlockBoosterPermanent`) вызывается извне — из магазина наград на стейте `RewardElements`.

## Состав модуля
- `BoostersSystem` — `IGameSystem`. Конструктор принимает `BoostersDescription`. В `Initialize` вызывает `Mechanic.InitializeBoosters()`, в `UpdateSystem` (только на `CalculateValue`) пересчитывает множитель.
- `BoostersDescription` — `ScriptableObject` (`CreateAssetMenu "Coffee/BoostersDescription"`) со списком `BoosterDescription`. Каждый `BoosterDescription`: `BoosterName`, `IsEnabled`, `Multiplier`, и условия — `RequiredElementNames`, `RequiredElementTypes`.
- `BoosterItem` — рантайм-представление бустера; считает применяемый множитель по текущему рецепту.
- `BoostersComponent` — состояние: ссылка на `BoostersDescription`, `CurrentMultiplier` (стартует `1f`), `AllBoosters`, `ActiveBoosters`.
- `BoostersMechanic` — инициализация, покупка, добавление/удаление, пересчёт множителя.
- Фабрика `GameObjectFactories/BoosterItemFactory` — превращает `BoosterDescription` в `BoosterItem`.

## Как работает (по коду)
1. **Инициализация** (`InitializeBoosters`): из описания через фабрику создаются все `BoosterItem`. Для каждого, если бустер уже куплен (см. `PlayerPrefs`), выставляется `IsEnabled = true`. В `ActiveBoosters` попадают все включённые:

```csharp
Component.AllBoosters = Component.BoostersDescription.Boosters
    .Select(BoosterItemFactory.CreateFromDescription)
    .ToList();
...
Component.ActiveBoosters = Component.AllBoosters
    .Where(booster => booster.IsEnabled)
    .ToList();
```

2. **Условие срабатывания** (`BoosterItem.GetAppliedMultiplier`): если бустер выключен — возвращает `1f`. Иначе проверяет, что все требуемые имена и все требуемые типы элементов присутствуют в рецепте; если хоть одно отсутствует — `1f`, иначе `Multiplier`:

```csharp
if (RequiredElementNames.Any(requiredElementName => !recipeElementNames.Contains(requiredElementName)))
    return 1f;
...
return Multiplier;
```

3. **Пересчёт за раунд** (`UpdateMechanic`): берёт элементы рецепта из `FillRecipeSystem` и перемножает множители всех активных бустеров:

```csharp
foreach (var booster in Component.ActiveBoosters)
    multiplier *= booster.GetAppliedMultiplier(fillRecipeSystem.Component.RecipeContainers);
Component.CurrentMultiplier = multiplier;
```

4. **Покупка навсегда** (`TryUnlockBoosterPermanent`): отказывает, если имя пустое или бустер уже куплен. Иначе находит бустер в `AllBoosters`, пишет флаг покупки в `PlayerPrefs` и активирует через `AddBooster`:

```csharp
PlayerPrefs.SetInt(GetBoosterPurchasedKey(boosterName), 1);
return AddBooster(booster);
```

5. **Хранение покупки**: ключ `PlayerPrefs` = префикс `"PurchasedBooster_"` + имя бустера. Чтение через `IsBoosterPurchased` (`GetInt(..., 0) == 1`).

6. **Add/Remove**: `AddBooster` добавляет в `ActiveBoosters` (если ещё нет) и ставит `IsEnabled = true`; `RemoveBooster` убирает из списка и снимает флаг. Возвращают `bool` об успехе.

> [!note] `InitializeBoosters` уже включает бустер по флагу `IsEnabled` из описания. Покупка через `PlayerPrefs` лишь дополнительно гарантирует включение ранее купленных, даже если в описании `IsEnabled = false`.

## Данные и зависимости
- **Конфиг:** `BoostersDescription` (`ScriptableObject`), передаётся в конструктор системы.
- **Читает из:** `FillRecipeSystem.Component.RecipeContainers` (имена и типы элементов рецепта).
- **Пишет в:** `BoostersComponent.CurrentMultiplier`, который читает `CalculateResultMechanic`.
- **Персистентность:** `PlayerPrefs`, ключи вида `PurchasedBooster_<имя>` (значение `0/1`).
- **Покупку инициирует:** магазин наград (`RewardElementsMechanic.BuyBoosterOffer`) на стейте `RewardElements`.

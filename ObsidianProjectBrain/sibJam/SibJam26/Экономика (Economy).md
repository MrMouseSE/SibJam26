---
tags: [фича, sibjam26]
type: note
---

# 💰 Экономика (Economy)

↑ [[Фичи]]

Кластер `GameSystemsScripts/EconomyScripts`.

## Что это
Кошелёк игрока: одна целочисленная валюта (`Balance`), которая загружается при старте, начисляется за победу и тратится в магазине наград. Состояние персистится в `PlayerPrefs`.

## Активные стейты
Своего «рабочего» стейта нет — `UpdateSystem` пустой. Система реактивная: её механику дёргают другие системы в любых стейтах. Фактически: начисление за победу и траты происходят на стейте `RewardElements` (магазин наград).

## Состав модуля
- `PlayerCurrencySystem` — `IGameSystem`. В `Initialize` загружает баланс (`Mechanic.Load()`); `UpdateSystem` и `DisposeSystem` пустые.
- `PlayerCurrencyComponent` — состояние: `int Balance` и константа ключа `PlayerPrefsCurrencyKey = "PlayerCurrency"`.
- `PlayerCurrencyMechanic` — операции над балансом: `Load`, `GetBalance`, `AddCurrency`, `TrySpend`, `Save`.

## Как работает (по коду)
1. **Загрузка** при `Initialize`:

```csharp
Component.Balance = PlayerPrefs.GetInt(PlayerCurrencyComponent.PlayerPrefsCurrencyKey, 0);
```

2. **Начисление** (`AddCurrency`) — игнорирует неположительные суммы, иначе прибавляет и сохраняет:

```csharp
if (value <= 0) return;
Component.Balance += value;
Save();
```

3. **Трата** (`TrySpend`) — `value <= 0` считается успехом без изменений; при нехватке возвращает `false`; иначе списывает и сохраняет:

```csharp
if (value <= 0) return true;
if (Component.Balance < value) return false;
Component.Balance -= value;
Save();
return true;
```

4. **Сохранение** (`Save`) — пишет баланс в `PlayerPrefs` по ключу `"PlayerCurrency"`.

> [!note] `TrySpend` — единственная точка проверки «хватает ли денег». Магазин наград откатывает покупку через `AddCurrency`, если списал деньги, но действие не удалось (например, бустер не разблокировался).

## Данные и зависимости
- **Персистентность:** `PlayerPrefs`, ключ `"PlayerCurrency"` (целое, по умолчанию `0`).
- **Кто начисляет:** `RewardElementsMechanic.EnterRewardState` — `AddCurrency(RewardShopDescription.CurrencyRewardPerWin)`.
- **Кто тратит:** `RewardElementsMechanic` (`BuyBoosterOffer`, `BuyIngredientOffer`, `BuySlotOffer`) — через `TrySpend(offer.Price)`.
- **Кто читает баланс:** `RewardElementsMechanic.RefreshUI` — `GetBalance()` для отображения и проверки доступности офферов.
- **Связанные фичи:** `Награды (Reward Elements)`.

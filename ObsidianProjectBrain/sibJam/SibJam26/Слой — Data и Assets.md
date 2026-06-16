---
tags: [архитектура, слой, sibjam26]
type: note
---

# 🗃️ Слой — Data и Assets

↑ [[Слои проекта]]

## Что это
Слой данных и контента: параметры игры вынесены из кода в ScriptableObject-ассеты, а тяжёлый контент и сцены грузятся через Addressables.

## Что делает
- Хранит конфигурацию геймплея отдельно от логики (дизайнер правит ассеты без перекомпиляции).
- Предоставляет системам их параметры на этапе регистрации (через конструкторы систем в Bootstrap).
- Сохраняет прогресс/инвентарь игрока между сессиями.

## Описания (ScriptableObject)
Прокинуты в `LoadGameComponent` и переданы соответствующим системам:
- `CoffeeDescription` — элементы, рецепты, стартовые элементы (`StartAvailableElements`, `StartElementCount`), `ElementsHand`. Используется `RecipeHandlerSystem`, `ElementsHandSystem`, `StaticElementFactory`.
- `BoostersDescription` — параметры бустеров (`BoostersSystem`).
- `RewardShopDescription` — предложения магазина наград (`RewardElementsSystem`).
- `DaysAchievementsDescription` — цели по дням (`LevelCompleteSystem`, `LevelHandlerSystem`, `RewardElementsSystem`).
- `AnimationsDescription` — тайминги анимаций, в т.ч. `Boil` (`GameSpeedSystem`, `BoilingProcessSystem`).

## Контент и сцены
- `Assets/Content/*` — префабы, звуки, текстуры.
- `Assets/AddressableAssetsData/*` — конфигурация Addressables.
- Сцены и звук грузятся как `AssetReference` через Addressables.

## Персистентность
- `PlayerPrefs` — стартовые/доступные элементы, вероятно баланс валюты.
- `ElementsStaticInventory` — статический инвентарь доступных элементов, заполняется на старте из `CoffeeDescription`.

## Роль в архитектуре
Источник правды по числовым параметрам и контенту. Логика (Domain Systems) не хардкодит значения, а читает их отсюда; это упрощает баланс и наполнение игры.

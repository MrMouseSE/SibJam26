---
tags: [фича, система, coffee, sibjam26]
type: note
---

# Система — ElementsHandSystem

↑ [[Элементы (Element System)]]

## Что это
Хранит и пересчитывает параметры «руки» игрока: текущую вместимость руки (`CurrentHandSlotCapacity`) и количество доступных пересдач (`CurrentSwapCount`). Является источником данных для отрисовки руки и для логики пересдачи.

## Активные стейты
Работает в любом стейте: в `UpdateSystem` нет проверки `CurrentGameState`. Каждый кадр вызывается `Mechanic.UpdateMechanic`, но реальная работа происходит только при поднятом флаге `Component.IsValuesUpdateNeed`.

## Состав модуля
- `ElementsHandSystem` — система, создаёт компонент и механику, в `Initialize` инициализирует и фиксирует значения руки.
- `ElementsHandMechanic` — механика: чтение из `PlayerPrefs`, клампинг и применение приростов.
- `ElementsHandComponent` — данные: текущие значения, отложенные приросты, ссылка на `ElementsHandDescription`.

## Как работает (по коду)
При инициализации значения читаются из `PlayerPrefs` и сразу клампятся в допустимый диапазон:

```csharp
public void Initialize(GameSystemsHandler gameSystemsHandler)
{
    Mechanic.InitializeHandValues();
    Mechanic.FixHandCounts();
}
```

```csharp
public void InitializeHandValues()
{
    Component.CurrentHandSlotCapacity = PlayerPrefs.GetInt("ElementsHandCapacity", 0);
    Component.CurrentSwapCount = PlayerPrefs.GetInt("ElementsHandSwapCount", 2);
}
```

`FixHandCounts` ограничивает значения границами из описания (`MinMaxElementsHandCapacity`, `MinMaxElementSwapCount`).

В апдейте приросты применяются лениво — только по флагу, после чего флаг и сами приросты сбрасываются:

```csharp
public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
{
    if (!Component.IsValuesUpdateNeed) return;
    Component.IsValuesUpdateNeed = false;
    Component.CurrentHandSlotCapacity += Component.AddHandSlotCapacityValue;
    Component.AddHandSlotCapacityValue = 0;
    Component.CurrentSwapCount += Component.AddSwapCountValue;
    Component.AddSwapCountValue = 0;
    FixHandCounts();
}
```

Поле `UnusedSwaps` хранит счётчик уже использованных пересдач в текущем выборе; им управляют другие системы (увеличивается при пересдаче, обнуляется при завершении выбора).

## Данные и зависимости
- `ElementsHandDescription` (ScriptableObject) — границы `MinMaxElementsHandCapacity` и `MinMaxElementSwapCount`, передаётся в конструктор системы.
- `PlayerPrefs` — ключи `ElementsHandCapacity` и `ElementsHandSwapCount`.
- Систему читают `ElementsDrawSystem` (вместимость руки = число раздаваемых элементов), `ElementsRedrawSystem` и `CompleteSelectionSystem` (поле `UnusedSwaps`).

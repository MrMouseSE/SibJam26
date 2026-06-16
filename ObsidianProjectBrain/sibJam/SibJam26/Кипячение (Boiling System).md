---
tags: [фича, coffee, sibjam26]
type: hub
---

# 🔥 Кипячение (Boiling System)

↑ [[Фичи]]

Кластер `CoffeeSystemsScripts/BoilingSystemScripts` (подпапки `StartBoilingScripts`, `BoilingProcessScripts`, `CompleteBoilingScripts`).

## Назначение
Мини-этап кипячения кофе между сравнением рецепта (`CompareRecipe`) и расчётом результата (`CalculateValue`). Игрок запускает кипячение, во время процесса растёт `BoilValue` (с шумом и цветовой/стрелочной индикацией), и завершает его кнопкой. По завершении из накопленного значения через кривую считается множитель `BoilMultiplier`, который влияет на расчёт результата.

## Активные стейты
- `StartBoil` — обрабатывает `StartBoilingSystem`.
- `BoilingCoffee` — обрабатывает `BoilingProcessSystem`.
- `CompleteBoilingSystem` крутится постоянно (без проверки стейта) и сам переводит игру на `CalculateValue`.

> [!note]
> Переход внутри фичи: `StartBoil` → (в `StartBoilingMechanic`) `BoilingCoffee` → (в `CompleteBoilingMechanic`, по клику или авто-экстриму) `CalculateValue`.

## Системы
- [[Система — StartBoilingSystem]] — запуск кипячения на стейте `StartBoil`.
- [[Система — BoilingProcessSystem]] — сам процесс нагрева и индикация на стейте `BoilingCoffee`.
- [[Система — CompleteBoilingSystem]] — кнопка завершения и переход к расчёту.

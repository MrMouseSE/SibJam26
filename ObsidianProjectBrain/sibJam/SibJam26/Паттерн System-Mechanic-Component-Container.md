---
tags: [паттерн, sibjam26]
type: hub
---

# 🧩 Паттерн System / Mechanic / Component / Container

↑ [[Паттерны]]

Базовая единица фичи в проекте. Четыре роли с чётким разделением ответственности — каждая описана отдельной нодой.

## Поток исполнения
1. Системы регистрируются в `GameSystemsHandler`.
2. `Initialize` у каждой системы.
3. В update-цикле — `UpdateSystem`.
4. Внутри `UpdateSystem` система фильтрует стейт и вызывает `Mechanic.UpdateMechanic`.
5. На остановке — `DisposeSystem` / `DisposeMechanic`.

## Роли
- [[Роль — System]] — когда выполнять логику.
- [[Роль — Mechanic]] — что делать.
- [[Роль — Component]] — что хранить.
- [[Роль — Container]] — мост к Unity-объектам.

## Как добавить новую фичу
1. Создать `FeatureSystem`, `FeatureMechanic`, `FeatureComponent` (и `FeatureContainer` при работе с объектами сцены).
2. Реализовать `IGameSystem` и `IGameMechanic`.
3. Зарегистрировать систему в `LoadGameComponent`.
4. Привязать к нужным стейтам.
5. Подключить контейнер в scene-композиции.
6. Добавить `Dispose`-очистку.

## Антипаттерны
- Смешивать логику стейтов и Unity-view в одном классе.
- Хранить Unity-ссылки в `System` / `Mechanic`.
- «Монолитная» система на несколько независимых фич.
- Создавать игровые объекты в обход фабрик.

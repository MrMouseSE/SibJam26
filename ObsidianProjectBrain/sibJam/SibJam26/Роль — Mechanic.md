---
tags: [паттерн, роль, sibjam26]
type: note
---

# ⚙️ Роль — Mechanic

↑ [[Паттерн System-Mechanic-Component-Container]]

## Что это
Поведенческий слой фичи. Содержит непосредственные действия: **что** делать, когда система решила, что пора.

## Контракт
`IGameMechanic` (`Assets/Scripts/GameSystemsScripts/IGameMechanic.cs`):
```csharp
public interface IGameMechanic
{
    void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime);
    void DisposeMechanic();
}
```

## Что делает
- Изменение данных, операции фичи, реакция на UI-события.
- Инициирует переходы `GameState` по завершении своего этапа.
- Подписывается на события контейнеров и отписывается в `DisposeMechanic`.
- Получает ссылку на свой `Component` обычно через конструктор; Unity-объекты — через `SetContainer(...)`.

## Как работает
- Не управляет глобальным циклом — её `UpdateMechanic` вызывает система.
- Часто хранит ссылку на `Component` для чтения/записи состояния.
- Типично содержит методы-сеттеры контейнеров, которые вызываются из scene-композиции (`SetButtonContainer`, `SetContainer`, `SetViewContainer` и т.п.).

## Правила
- Сюда выносится вся прикладная логика — её проще тестировать отдельно от кадра.
- Меняет стейт через `GameStateSystem`/`Mechanic`, а не «втихую».

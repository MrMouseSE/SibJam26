# SKILL: Добавление новой механики (System-Mechanic-Component-Container)

## 1) Назначение

Этот skill описывает стандартный процесс, по которому нужно добавлять новую фичу по пользовательскому описанию в архитектуре:

- `System` — управляет выполнением по состояниям;
- `Mechanic` — выполняет действия над сущностями;
- `Component` — хранит данные фичи;
- `Container` — хранит ссылки на Unity-компоненты сцены.

Документ предназначен как рабочая инструкция для реализации фичи от запроса до интеграции.

## 2) Входные данные от пользователя

Перед реализацией нужно извлечь из описания пользователя:

1. Цель фичи: что должно происходить.
2. Условия запуска: в каких стейтах работает фича.
3. Действия: что механика меняет в данных/объектах.
4. Связь с Unity: какие ссылки нужны (`Transform`, `Renderer`, UI и т.д.).
5. Переходы состояний: в какой стейт переводит фича и при каких условиях.
6. Создание объектов: какие объекты нужно создавать через фабрики.

Если часть пунктов не указана явно, использовать разумные предположения и фиксировать их в итоговом отчете.

## 3) Обязательные архитектурные правила

1. Никакой доменной логики в `System`: только orchestration и проверки state.
2. `Mechanic` содержит бизнес-поведение фичи.
3. `Component` хранит данные и флаги фичи.
4. Unity-ссылки хранятся в `Container`.
5. Новые игровые объекты создаются только через статические фабрики.
6. Фабрики размещаются только в единственной папке:
   - `Assets/Scripts/GameObjectFactories`
7. Нельзя создавать дополнительные папки `GameObjectFactories` в других местах.

## 4) Стандартная структура новой фичи

Новая фича должна быть оформлена отдельным модулем в `Assets/Scripts/GameSystemsScripts/...`:

- `FeatureSystem.cs`
- `FeatureMechanic.cs`
- `FeatureComponent.cs`
- `FeatureContainer.cs` (если нужны Unity-ссылки)

Дополнительно при необходимости:

- `FeatureEvents.cs` (если есть события фичи)
- `FeatureConfig.cs` (если нужны параметры/конфиг)

## 5) Алгоритм реализации (пошагово)

1. Проанализировать описание фичи и выделить:
   - state-условия;
   - действия;
   - входы/выходы.
2. Создать `Component`:
   - флаги;
   - runtime-данные;
   - ссылки на сущности фичи.
3. Создать `Mechanic`:
   - реализовать `IGameMechanic`;
   - реализовать `UpdateMechanic(...)`;
   - добавить `DisposeMechanic()` для отписок.
4. Создать `System`:
   - реализовать `IGameSystem`;
   - в `UpdateSystem(...)` проверять `GameState` и флаги;
   - делегировать работу в `Mechanic`.
5. Создать `Container` (если нужен):
   - только Unity-ссылки;
   - без тяжелой бизнес-логики.
6. Встроить фичу в bootstrap:
   - зарегистрировать систему в composition root (`LoadGameComponent` или аналог).
7. Подключить container на уровне сцены:
   - через `SceneSystemsContainer`/`ISceneRoot` wiring.
8. Если создаются новые игровые объекты:
   - добавить/расширить статическую фабрику в `Assets/Scripts/GameObjectFactories`;
   - использовать фабрику из механики.
9. Обновить state-flow:
   - при необходимости добавить новый `GameState`;
   - обновить переходы в механиках.
10. Проверить disposal:
   - все подписки и временные ресурсы корректно освобождаются.
11. Обновить документацию:
   - архитектурный документ;
   - таблицу стейтов/фич.

## 6) Шаблон проектирования state-логики

Для каждой новой фичи явно зафиксировать:

- `Active States`: в каких стейтах система активна;
- `Enter Condition`: условия входа в state/выполнение;
- `Execute Condition`: условия продолжения работы;
- `Exit Condition`: условия завершения этапа;
- `Next State`: куда переводит фича.

Если фича не меняет state, это должно быть указано явно.

## 7) Мини-шаблоны классов

```csharp
// FeatureSystem.cs
public class FeatureSystem : IGameSystem
{
    public FeatureComponent Component;
    public FeatureMechanic Mechanic;

    public FeatureSystem(/* deps */)
    {
        Component = new FeatureComponent(/* deps */);
        Mechanic = new FeatureMechanic(Component /*, deps */);
    }

    public void Initialize(GameSystemsHandler gameSystemsHandler) { }

    public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
    {
        // 1) state checks
        // 2) guard checks
        // 3) delegate to mechanic
        Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
    }

    public void DisposeSystem()
    {
        Mechanic.DisposeMechanic();
    }
}
```

```csharp
// FeatureMechanic.cs
public class FeatureMechanic : IGameMechanic
{
    private readonly FeatureComponent _component;

    public FeatureMechanic(FeatureComponent component)
    {
        _component = component;
    }

    public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
    {
        // Main feature behavior
    }

    public void DisposeMechanic()
    {
        // Unsubscribe/cleanup
    }
}
```

```csharp
// FeatureComponent.cs
public class FeatureComponent
{
    // Runtime state and feature data only
}
```

```csharp
// FeatureContainer.cs
public class FeatureContainer : MonoBehaviour
{
    // Unity references only: Transform, Renderer, UI, etc.
}
```

## 8) Правила принятия решений при неоднозначном описании

Если описание пользователя неполное:

1. Сначала использовать текущие архитектурные правила как приоритет.
2. Минимизировать связность и количество новых зависимостей.
3. Не вводить новый state без явной необходимости.
4. При риске архитектурного конфликта остановиться и запросить уточнение по одному критичному вопросу.

## 9) Definition of Done для новой фичи

Фича считается добавленной, если:

1. Есть `System/Mechanic/Component` (+ `Container` при необходимости).
2. Система зарегистрирована и реально участвует в update-loop.
3. State-ограничения корректно заданы.
4. Unity-ссылки вынесены в контейнер.
5. Создание новых объектов идет через `GameObjectFactories`.
6. `Dispose` покрывает подписки/ресурсы.
7. Документация обновлена.

## 10) Формат отчета пользователю после реализации

После добавления фичи в ответе указывать:

1. Какие файлы созданы/изменены.
2. Какие state-условия добавлены/изменены.
3. Какие фабрики использованы/добавлены.
4. Какие допущения были сделаны.
5. Какие тесты/проверки выполнены и что не проверено.


---
tags: [приём, sibjam26]
type: note
---

# ⚙️ UniTask update-loop

↑ [[Приёмы]]

## Суть
Вместо `MonoBehaviour.Update()` на каждой системе — единый цикл в `GameSystemsHandler` на `UniTask`:

```csharp
private async UniTask UpdateSystems(CancellationToken token)
{
    while (_isProcess)
    {
        foreach (var gameSystem in GameSystems)
            gameSystem.Value.UpdateSystem(this, Time.deltaTime);
        await UniTask.Yield();
    }
}
```

- Запускается в конструкторе `GameSystemsHandler` через `.Forget()`.
- Кадр за кадром вызывает `UpdateSystem` у всех систем, передавая `Time.deltaTime`.
- Останавливается через `_isProcess = false` + `CancellationTokenSource.Cancel()` в `DisposeSystems`.

## Зачем
- Один контролируемый порядок обновления вместо россыпи `MonoBehaviour`.
- Системы — обычные C#-классы (легче тестировать, не привязаны к Unity-объектам).
- Централизованный старт/стоп жизненного цикла.

> [!warning] Риск: коллекция `GameSystems` обходится прямо в цикле — добавление/удаление систем во время обхода может бросить исключение.

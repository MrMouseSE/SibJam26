# SibJam26 — short context

## Что это

- Unity 6 проект (`6000.3.13f1`) про кофе-геймплей.
- Базовая архитектура: `IGameSystem` + `System/Mechanic/Component`.

## Быстрые пути

- Код: `Assets/Scripts`
- Сцены: `Assets/Scenes`
- Игровые данные (SO): `Assets/Descriptions`
- Контент (prefab/sound/texture): `Assets/Content`
- Пакеты: `Packages/manifest.json`

## Точки входа

- Bootstrap: `Assets/Scripts/GameStartupScripts/LoadGameComponent.cs`
- Загрузка/переключение сцен: `Assets/Scripts/GameStartupScripts/SceneLoadingHandler.cs`
- Имена сцен: `Assets/Scripts/ScenesOperatingScripts/SceneNamesConst.cs`
- Менеджер систем: `Assets/Scripts/ScenesOperatingScripts/GameSystemsHandler.cs`

## Основной flow состояний

`StartGame -> DrawElements -> SelectElements -> CompareRecipe -> StartBoil -> BoilingCoffee -> CalculateValue -> ApproveRecipe -> CoffeeShow -> CompareLevelComplete -> RewardElements -> ChangeLevel -> UpdateLevelView`

## Системы по состояниям

- `StartGame`: `StartGameSystem`
- `DrawElements`: `ElementsDrawSystem`
- `SelectElements`: `SelectionButtonsSystem`, `ElementsRedrawSystem`, `ElementSelectionSystem`, `CompleteSelectionSystem`, `FillRecipeSystem`
- `CompareRecipe`: `CompareRecipeSystem`
- `StartBoil`: `StartBoilingSystem`
- `BoilingCoffee`: `BoilingProcessSystem`
- `CalculateValue`: `CalculateResultSystem`, `BoostersSystem`
- `ApproveRecipe`: `ApproveRecipeSystem`
- `CoffeeShow`: `ShowCoffeeSystem`
- `CompareLevelComplete`: `LevelCompleteSystem`
- `RewardElements`: `RewardElementsSystem`
- `ChangeLevel`: `LevelHandlerSystem`
- `UpdateLevelView`: `LevelViewSystem`
- Почти всегда активна: `InterfaceScoreSystem` (кроме `StartGame`)

## Важные заметки

- В Build Settings есть `Assets/Scenes/MainMenu.unity` (disabled), а в проекте используется `MainMenuScene.unity`.
- При изменении состава систем/сцен обновляй `PROJECT_CONTEXT.md` и этот файл.

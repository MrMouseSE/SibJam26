using AnimationDescriptionsScripts;
using CameraScripts;
using CoffeeScripts;
using CoffeeScripts.ElementsInventoryScripts;
using GameSystemsScripts.CameraSystem;
using GameSystemsScripts.CoffeeSystemsScripts.BoilingSystemScripts.BoilingProcessScripts;
using GameSystemsScripts.CoffeeSystemsScripts.BoilingSystemScripts.CompleteBoilingScripts;
using GameSystemsScripts.CoffeeSystemsScripts.BoilingSystemScripts.StartBoilingScripts;
using GameSystemsScripts.CoffeeSystemsScripts.CalculateResultSystemScripts;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.CompleteSelectionSystemScripts;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsDrawScripts;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementSelectionScripts;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsHandSystemScripts;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsRedrawScripts;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.SelectionButtonsScripts;
using GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.AproveRecipeScripts;
using GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.CompareRecipeScripts;
using GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.FillRecipeScripts;
using GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.RecipeHandlerScripts;
using GameSystemsScripts.CoffeeSystemsScripts.RewardElementsSystemScripts;
using GameSystemsScripts.GameInputScripts;
using GameSystemsScripts.GameSpeedScripts;
using GameSystemsScripts.GameStateScripts;
using GameSystemsScripts.LevelsSystemScripts.GameCompleteSystemScripts;
using GameSystemsScripts.LevelsSystemScripts.LevelCompleteScripts;
using GameSystemsScripts.LevelsSystemScripts.LevelHandlerScripts;
using LevelAchievementsScripts;
using MainMenuScripts.SettingsHanlderScripts;
using ScenesOperatingScripts;
using SoundsComponentsScripts;
using SupportScripts;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;

namespace GameStartupScripts
{
    public class LoadGameComponent : MonoBehaviour
    {
        public AssetReference SoundObjectReference;
        public AssetReference[] ScenesLoadAtStart;
        public AudioMixer GameAudioMixer;
        public SettingsHandlerContainer SettingsHandler;
        public CameraContainer CameraContainer;
        public AnimationsDescription AnimationsDescription;
        public CoffeeDescription CoffeeDescription;
        public DaysAchievementsDescription DaysAchievementsDescription;
        
        private GameSystemsHandler _gameSystemsHandler;
        private AudioMixerHandler _audioMixerHandler;
        
        public async void Awake()
        {
            await SoundInstancerController.SetRefObjects(SoundObjectReference, GameAudioMixer);
            StaticElementFactory.CoffeeDescription = CoffeeDescription;
            _gameSystemsHandler = new GameSystemsHandler();
            
            _gameSystemsHandler.AddStateSystem(new GameStateSystem());
            _gameSystemsHandler.AddGameSystem(new GameCameraSystem(CameraContainer));
            _gameSystemsHandler.AddGameSystem(new GameInputSystem());
            _gameSystemsHandler.AddGameSystem(new GameSpeedSystem(AnimationsDescription));
            _gameSystemsHandler.AddGameSystem(new RecipeHandlerSystem(CoffeeDescription));
            
            _gameSystemsHandler.AddGameSystem(new ElementsHandSystem(CoffeeDescription.ElementsHand));
            _gameSystemsHandler.AddGameSystem(new ElementsDrawSystem());
            _gameSystemsHandler.AddGameSystem(new SelectionButtonsSystem());
            _gameSystemsHandler.AddGameSystem(new ElementsRedrawSystem());
            _gameSystemsHandler.AddGameSystem(new ElementSelectionSystem());
            _gameSystemsHandler.AddGameSystem(new CompleteSelectionSystem());
                
            _gameSystemsHandler.AddGameSystem(new FillRecipeSystem());
            _gameSystemsHandler.AddGameSystem(new CompareRecipeSystem());
            
            _gameSystemsHandler.AddGameSystem(new StartBoilingSystem());
            _gameSystemsHandler.AddGameSystem(new BoilingProcessSystem(AnimationsDescription.Boil));
            _gameSystemsHandler.AddGameSystem(new CompleteBoilingSystem());
            
            _gameSystemsHandler.AddGameSystem(new CalculateResultSystem());
            _gameSystemsHandler.AddGameSystem(new ApproveRecipeSystem());
            _gameSystemsHandler.AddGameSystem(new RewardElementsSystem(DaysAchievementsDescription));
            
            _gameSystemsHandler.AddGameSystem(new LevelCompleteSystem(DaysAchievementsDescription));
            _gameSystemsHandler.AddGameSystem(new LevelHandlerSystem(DaysAchievementsDescription));
            _gameSystemsHandler.AddGameSystem(new GameCompleteSystem());
            
            _audioMixerHandler = new AudioMixerHandler(GameAudioMixer);
            SettingsHandler.Mixer = _audioMixerHandler;
            SceneLoadingHandler.LoadScenes(ScenesLoadAtStart);
            SceneLoadingHandler.OnSceneLoaded += SetMenuSceneActive;
            ElementsStaticInventory.LoadCurrentAvailableElements(CoffeeDescription);
            _gameSystemsHandler.InitializeSystems();
            
        }

        private void SetMenuSceneActive()
        {
            SceneLoadingHandler.OnSceneLoaded -= SetMenuSceneActive;
            foreach (var sceneRoot in SceneLoadingHandler.SceneRoots)
            {
                sceneRoot.Value.Item2.InitializeSceneSystems(_gameSystemsHandler);
            }
            SceneLoadingHandler.SetSceneActive(SceneNamesConst.MainMenuScene);
            var cameraSystem = _gameSystemsHandler.GetGameSystem(typeof(GameCameraSystem)) as GameCameraSystem;
            cameraSystem.Component.CurrentCameraHolder = 
                SceneLoadingHandler.SceneRoots[SceneNamesConst.MainMenuScene].Item2.GetCameraHolder();
            cameraSystem.Component.IsCameraUpdating = true;
        }
    }
}

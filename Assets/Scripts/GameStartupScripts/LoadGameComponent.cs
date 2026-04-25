using CameraScripts;
using GameSystemsScripts.CameraSystem;
using GameSystemsScripts.GameInputScripts;
using MainMenuScripts.SettingsHanlderScripts;
using ScenesOperatingScripts;
using SoundsComponentsScripts;
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
        
        private GameSystemsHandler _gameSystemsHandler;
        private AudioMixerHandler _audioMixerHandler;
        
        public async void Awake()
        {
            await SoundInstancerController.SetRefObjects(SoundObjectReference, GameAudioMixer);
            _gameSystemsHandler = new GameSystemsHandler();
            _gameSystemsHandler.AddCameraSystem(new GameCameraSystem(CameraContainer));
            _gameSystemsHandler.AddGameSystem(new GameInputSystem());
            _audioMixerHandler = new AudioMixerHandler(GameAudioMixer);
            SettingsHandler.Mixer = _audioMixerHandler;
            SceneLoadingHandler.LoadScenes(ScenesLoadAtStart);
            SceneLoadingHandler.OnSceneLoaded += SetMenuSceneActive;
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

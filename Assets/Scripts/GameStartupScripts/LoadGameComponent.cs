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
        
        private GameSystemsHandler _gameSystemsHandler;
        private AudioMixerHandler _audioMixerHandler;
        
        public async void Awake()
        {
            await SoundInstancerController.SetRefObject(SoundObjectReference);
            _gameSystemsHandler = new GameSystemsHandler();
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
        }
    }
}

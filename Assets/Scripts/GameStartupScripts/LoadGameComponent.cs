using ScenesOperatingScripts;
using SoundsComponentsScripts;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameStartupScripts
{
    public class LoadGameComponent : MonoBehaviour
    {
        public AssetReference SoundObjectReference;
        public AssetReference[] ScenesLoadAtStart;
        
        private GameSystemsHandler _gameSystemsHandler;
        
        public void Awake()
        {
            _gameSystemsHandler = new GameSystemsHandler();
            SoundMixerController.SetRefObject(SoundObjectReference);
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

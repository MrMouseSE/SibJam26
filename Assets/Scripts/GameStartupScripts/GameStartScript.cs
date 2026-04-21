using System.Linq;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace GameStartupScripts
{
    
    public class GameStartScript
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void InitializeGame()
        {
            AddressableAssetGroup group = AddressableAssetSettingsDefaultObject.Settings.FindGroup("Scenes");
            SceneLoadingHandler.LoadScenes(group.entries.ToArray());
            SceneLoadingHandler.OnSceneLoaded += SetMenuSceneActive;
        }

        private static void SetMenuSceneActive()
        {
            SceneLoadingHandler.OnSceneLoaded -= SetMenuSceneActive;
        }
    }
}

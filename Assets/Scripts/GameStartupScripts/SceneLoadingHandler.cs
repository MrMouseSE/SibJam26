using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ScenesOperatingScripts;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace GameStartupScripts
{
    public static class SceneLoadingHandler
    {
        public static Dictionary<Scene, SceneRootHolder> SceneRoots = new();
    
        public static Action OnSceneLoaded;

        public static async void LoadScenes(AddressableAssetEntry[] scenesEntries)
        {
            Task[] tasks = new Task[scenesEntries.Length];
            for (int i = 0; i < scenesEntries.Length; i++)
            {
                tasks[i] = LoadScenes(scenesEntries[i].MainAsset.name);
            }
            await Task.WhenAll(tasks);
            OnSceneLoaded?.Invoke();
        }

        private static async Task<SceneInstance> LoadScenes(string sceneName)
        {
            var h = Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive, false);
            await h.Task;
            SceneRoots.Add(h.Result.Scene, h.Result.Scene.GetRootGameObjects()[0].GetComponent<SceneRootHolder>());
            return h.Result;
        }
    }
}
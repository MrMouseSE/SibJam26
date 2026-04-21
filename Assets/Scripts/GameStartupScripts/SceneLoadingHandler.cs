using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ScenesOperatingScripts;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace GameStartupScripts
{
    public static class SceneLoadingHandler
    {
        public static Dictionary<Scene, SceneRootHolder> SceneRoots = new();
    
        public static Action OnSceneLoaded;

        public static async void LoadScenes()
        {
            var scenesLoadEntry = Addressables.LoadAssetsAsync<Scene>("scene", 
                ad =>
                {
                    var h = Addressables.LoadSceneAsync(ad, LoadSceneMode.Additive, false);
                    SceneRoots.Add(h.Result.Scene, h.Result.Scene.GetRootGameObjects()[0].GetComponent<SceneRootHolder>());
                }, Addressables.MergeMode.Union, false);
            await scenesLoadEntry.Task;
            OnSceneLoaded?.Invoke();
            /*
            Task[] tasks = new Task[scenesLoadEntry.Result.Count];
            for (int i = 0; i < scenesLoadEntry.Result.Count; i++)
            {
                tasks[i] = LoadScenes(scenesLoadEntry.Result[i].name);
            }
            await Task.WhenAll(tasks);
            */
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
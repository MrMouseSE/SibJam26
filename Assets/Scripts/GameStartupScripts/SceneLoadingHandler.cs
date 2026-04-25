using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ScenesOperatingScripts;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace GameStartupScripts
{
    public static class SceneLoadingHandler
    {
        public static Dictionary<string, (AsyncOperationHandle<SceneInstance>, ISceneRoot)> SceneRoots = new();
    
        public static Action OnSceneLoaded;

        public static async void LoadScenes(AssetReference[] scenesLoadAtStart)
        {
            Task[] tasks = new Task[scenesLoadAtStart.Length];
            for (var index = 0; index < scenesLoadAtStart.Length; index++)
            {
                var location = scenesLoadAtStart[index];
                tasks[index] = LoadScenes(location);
            }
            await Task.WhenAll(tasks);
            OnSceneLoaded?.Invoke();
        }

        private static async Task<SceneInstance> LoadScenes(AssetReference sceneLocation)
        {
            var handle = Addressables.LoadSceneAsync(sceneLocation, LoadSceneMode.Additive, true);
            await handle.Task;
            var rootObject = handle.Result.Scene.GetRootGameObjects()[0].GetComponent<ISceneRoot>();
            SceneRoots.Add(handle.Result.Scene.name, new ValueTuple<AsyncOperationHandle<SceneInstance>, ISceneRoot>(handle, rootObject));
            rootObject.SetSceneObjectsActive(false);
            return handle.Result;
        }

        public static ISceneRoot SetSceneActive(string sceneName)
        {
            ISceneRoot root = null;
            foreach (var sceneRoot in SceneRoots)
            {
                sceneRoot.Value.Item2.SetSceneObjectsActive(sceneRoot.Key == sceneName);
                if (sceneRoot.Key == sceneName)
                    root = sceneRoot.Value.Item2;
            }
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
            return root;
        }
    }
}
using UnityEngine;

namespace GameStartupScripts
{
    
    public class GameStartScript
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void InitializeGame()
        {
            SceneLoadingHandler.LoadScenes();
            SceneLoadingHandler.OnSceneLoaded += SetMenuSceneActive;
        }

        private static void SetMenuSceneActive()
        {
            SceneLoadingHandler.OnSceneLoaded -= SetMenuSceneActive;
            Debug.Log("Loading menu scene");
        }
    }
}

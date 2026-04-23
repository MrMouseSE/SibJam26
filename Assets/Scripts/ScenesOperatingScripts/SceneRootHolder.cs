using System.Collections.Generic;
using SoundsComponentsScripts;
using UnityEngine;

namespace ScenesOperatingScripts
{
    public class SceneRootHolder : MonoBehaviour, ISceneRoot
    {
        public List<GameObject> VisibleObjects;
        
        public AreaMusicContainer SceneMusicContainer;
        
        [Space]
        public SceneSystemsContainer SceneSystemsContainer;

        public bool TryRemoveObjectFromVisibility(GameObject go)
        {
            return VisibleObjects.Remove(go);
        }

        public void AddObjectToSceneVisibility(GameObject go)
        {
            VisibleObjects.Add(go);
        }

        public void SetSceneObjectsActive(bool active)
        {
            SceneMusicContainer?.SwitchMusic(active);
            
            foreach (var visibleObject in VisibleObjects)
            {
                visibleObject.SetActive(active);
            }
        }

        public void InitializeSceneSystems(GameSystemsHandler systemsHandler)
        {
            SceneSystemsContainer.InitializeSceneSystems(systemsHandler);
        }
    }
}

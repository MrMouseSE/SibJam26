using System.Collections.Generic;
using UnityEngine;

namespace ScenesOperatingScripts
{
    public class SceneRootHolder : MonoBehaviour, ISceneRoot
    {
        public List<GameObject> VisibleObjects;

        public bool TryRemoveObjectFromVisibility(GameObject go)
        {
            return VisibleObjects.Remove(go);
        }

        public void AddObjectToSceneVisibility(GameObject go)
        {
            VisibleObjects.Add(go);
        }

        public void SetSceneObjectsVisibility(bool visible)
        {
            foreach (var visibleObject in VisibleObjects)
            {
                visibleObject.SetActive(visible);
            }
        }

        public void InitializeSceneSystems()
        {
        }
    }
}

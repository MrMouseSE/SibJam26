using UnityEngine;

namespace ScenesOperatingScripts
{
    public interface ISceneRoot
    {
        public bool TryRemoveObjectFromVisibility(GameObject go);
        public void AddObjectToSceneVisibility(GameObject go);
        public void SetSceneObjectsActive(bool active);
        public void InitializeSceneSystems(GameSystemsHandler systemsHandler);
    }
}
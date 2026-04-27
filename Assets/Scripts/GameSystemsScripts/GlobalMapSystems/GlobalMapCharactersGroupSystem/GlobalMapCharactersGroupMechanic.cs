using ScenesOperatingScripts;

namespace GameSystemsScripts.GlobalMapSystems.GlobalMapCharactersGroupSystem
{
    public class GlobalMapCharactersGroupMechanic : IGameMechanic
    {
        public GlobalMapCharactersGroupComponent Component;
        public GlobalMapCharactersGroupMechanic(GlobalMapCharactersGroupComponent component)
        {
            Component = component;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            throw new System.NotImplementedException();
        }

        public void DisposeMechanic()
        {
            throw new System.NotImplementedException();
        }
    }
}
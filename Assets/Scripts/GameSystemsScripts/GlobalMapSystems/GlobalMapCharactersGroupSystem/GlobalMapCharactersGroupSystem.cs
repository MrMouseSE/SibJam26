using ScenesOperatingScripts;

namespace GameSystemsScripts.GlobalMapSystems.GlobalMapCharactersGroupSystem
{
    public class GlobalMapCharactersGroupSystem : IGameSystem
    {
        public GlobalMapCharactersGroupMechanic Mechanic;
        public GlobalMapCharactersGroupComponent Component;

        public GlobalMapCharactersGroupSystem()
        {
            Component = new();
            Mechanic = new(Component);
        }
        
        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
        }
    }
}
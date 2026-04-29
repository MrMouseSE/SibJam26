using ScenesOperatingScripts;

namespace GameSystemsScripts.GameCharactersSystems.AdventureResultSystem
{
    public class AdventureResultSystem : IGameSystem
    {
        public AdventureResultComponent Component;
        public AdventureResultMechanic Mechanic;

        public AdventureResultSystem()
        {
            Component = new AdventureResultComponent();
            Mechanic = new AdventureResultMechanic(Component);
        }
        
        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if(Component.CharactersGroups.Count == 0) return;
            Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
        }
    }
}
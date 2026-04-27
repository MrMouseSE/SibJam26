using ScenesOperatingScripts;

namespace GameSystemsScripts.GameCharactersSystems.AdventureResultSystem
{
    public class AdventureResultMechanic : IGameMechanic
    {
        public AdventureResultComponent Component;

        public AdventureResultMechanic(AdventureResultComponent component)
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
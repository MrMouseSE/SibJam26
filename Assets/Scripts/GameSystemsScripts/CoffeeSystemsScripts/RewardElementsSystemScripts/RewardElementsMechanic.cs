using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.RewardElementsSystemScripts
{
    public class RewardElementsMechanic : IGameMechanic
    {
        public RewardElementsComponent Component;

        public RewardElementsMechanic(RewardElementsComponent component)
        {
            Component = component;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            
        }

        public void DisposeMechanic()
        {
        }
    }
}
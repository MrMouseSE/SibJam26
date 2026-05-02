using GameScripts;
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
            //TODO: reward element to player
            
            gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.CompareLevelComplete);
        }

        public void DisposeMechanic()
        {
        }
    }
}
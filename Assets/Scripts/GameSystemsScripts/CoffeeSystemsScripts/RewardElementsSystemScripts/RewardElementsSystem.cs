using GameScripts;
using LevelAchievementsScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.RewardElementsSystemScripts
{
    public class RewardElementsSystem : IGameSystem
    {
        public RewardElementsComponent Component;
        public RewardElementsMechanic Mechanic;

        public RewardElementsSystem(DaysAchievementsDescription dayAchievementsDescription)
        {
            Component = new RewardElementsComponent(dayAchievementsDescription);
            Mechanic = new RewardElementsMechanic(Component);
        }
        
        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.RewardElements) return;
            Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
        }
    }
}
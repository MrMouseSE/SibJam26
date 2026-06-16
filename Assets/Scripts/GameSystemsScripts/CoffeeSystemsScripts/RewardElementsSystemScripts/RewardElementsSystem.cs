using GameScripts;
using LevelAchievementsScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.RewardElementsSystemScripts
{
    public class RewardElementsSystem : IGameSystem
    {
        public RewardElementsComponent Component;
        public RewardElementsMechanic Mechanic;

        public RewardElementsSystem(DaysAchievementsDescription dayAchievementsDescription, RewardShopDescription rewardShopDescription)
        {
            Component = new RewardElementsComponent(dayAchievementsDescription, rewardShopDescription);
            Mechanic = new RewardElementsMechanic(Component);
        }

        public void Initialize(GameSystemsHandler gameSystemsHandler)
        {
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

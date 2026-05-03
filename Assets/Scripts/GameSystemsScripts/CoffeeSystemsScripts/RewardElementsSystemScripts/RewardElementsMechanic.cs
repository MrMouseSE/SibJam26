using CoffeeScripts.ElementsInventoryScripts;
using GameScripts;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsHandSystemScripts;
using GameSystemsScripts.LevelsSystemScripts.LevelCompleteScripts;
using GameSystemsScripts.LevelsSystemScripts.LevelHandlerScripts;
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
            var levelCompleteSystem = (LevelCompleteSystem)gameSystemsHandler.GetGameSystem(typeof(LevelCompleteSystem));
            var levelHandler = (LevelHandlerSystem)gameSystemsHandler.GetGameSystem(typeof(LevelHandlerSystem));
            var handSystem = (ElementsHandSystem)gameSystemsHandler.GetGameSystem(typeof(ElementsHandSystem));
            var handlerComponenet = levelHandler.Component;
            
            var completeComponent = levelCompleteSystem.Component;
            if (completeComponent.IsLevelCompleted)
            {
                var levelAchievementDescription = handlerComponenet.GetLevelAchievementDescription();
                var levelAchive = levelAchievementDescription;
                foreach (var bonus in levelAchive.BonusElementsForAchieved)
                {
                    ElementsStaticInventory.AddElementToInventory(bonus.ElementName);
                }

                handSystem.Component.IsValuesUpdateNeed = true;
                handSystem.Component.AddHandSlotCapacityValue = levelAchievementDescription.AdditionalHandSlot;
                handSystem.Component.AddSwapCountValue = levelAchievementDescription.AdditionalSwap;
            }
            
            gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.ChangeLevel);
        }

        public void DisposeMechanic()
        {
        }
    }
}
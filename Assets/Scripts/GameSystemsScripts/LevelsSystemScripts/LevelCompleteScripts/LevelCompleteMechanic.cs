using GameScripts;
using GameSystemsScripts.CoffeeSystemsScripts.CalculateResultSystemScripts;
using GameSystemsScripts.LevelsSystemScripts.LevelHandlerScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.LevelsSystemScripts.LevelCompleteScripts
{
    public class LevelCompleteMechanic : IGameMechanic
    {
        public LevelCompleteComponent Component;

        public LevelCompleteMechanic(LevelCompleteComponent component)
        {
            Component = component;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            var resultSystem = (CalculateResultSystem)gameSystemsHandler.GetGameSystem(typeof(CalculateResultSystem));
            var levelHandlerComponent = ((LevelHandlerSystem)gameSystemsHandler.GetGameSystem(typeof(LevelHandlerSystem))).Component;
            
            if (resultSystem.Component.ResultValue > 
                Component.DaysDescription.DaysAchievementsDescriptions[levelHandlerComponent.Day].
                    LevelsAchievements[levelHandlerComponent.Level].LevelScoreToAchieve)
            {
                Component.IsLevelCompleted = true;
            }
            
            gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.RewardElements);
            //TODO: not complete restart levels
        }

        public void DisposeMechanic()
        {
        }
    }
}
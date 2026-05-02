using GameScripts;
using LevelAchievementsScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.LevelsSystemScripts.LevelCompleteScripts
{
    public class LevelCompleteSystem : IGameSystem
    {
        public LevelCompleteComponent Component;
        public LevelCompleteMechanic Mechanic;

        public LevelCompleteSystem(DaysAchievementsDescription description)
        {
            Component = new LevelCompleteComponent(description);
            Mechanic = new LevelCompleteMechanic(Component);
        }

        public void Initialize(GameSystemsHandler gameSystemsHandler)
        {
        }

        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.CompareLevelComplete) return;
            Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
        }
    }
}
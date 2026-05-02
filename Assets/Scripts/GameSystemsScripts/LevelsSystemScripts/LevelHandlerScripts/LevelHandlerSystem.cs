using GameScripts;
using LevelAchievementsScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.LevelsSystemScripts.LevelHandlerScripts
{
    public class LevelHandlerSystem : IGameSystem
    {
        public LevelHandlerComponent Component;
        public LevelHandlerMechanic Mechanic;

        public LevelHandlerSystem(DaysAchievementsDescription description)
        {
            Component = new LevelHandlerComponent(description);
            Mechanic = new LevelHandlerMechanic(Component);
        }

        public void Initialize(GameSystemsHandler gameSystemsHandler)
        {
        }

        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.ChangeLevel) return;
            Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
        }
    }
}
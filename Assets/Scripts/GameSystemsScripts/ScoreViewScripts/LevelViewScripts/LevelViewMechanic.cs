using GameScripts;
using GameSystemsScripts.LevelsSystemScripts.LevelHandlerScripts;
using GameSystemsScripts.ScoreViewScripts.InterfaceScoreScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.ScoreViewScripts.LevelViewScripts
{
    public class LevelViewMechanic : IGameMechanic
    {
        public LevelViewComponent Component;

        public LevelViewMechanic(LevelViewComponent component)
        {
            Component = component;
        }

        public void SetViewContainer(LevelViewContainer container)
        {
            Component.Container = container;
            Component.Container.DayIndex.text = "1";
            Component.Container.LevelIndex.text = "1";
        }
        
        public void ShowRequiredScore(GameSystemsHandler gameSystemsHandler)
        {
            var scoreSystem = (InterfaceScoreSystem)gameSystemsHandler.GetGameSystem(typeof(InterfaceScoreSystem));
            scoreSystem.Mechanic.ShowRequiredScore(gameSystemsHandler);
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            var levelSystem = (LevelHandlerSystem)gameSystemsHandler.GetGameSystem(typeof(LevelHandlerSystem));
            var day = levelSystem.Component.DaysDescription.DaysAchievementsDescriptions[levelSystem.Component.Day];
            Component.Container.DayIndex.text = day.DayIndex.ToString();
            var level = day.LevelsAchievements[levelSystem.Component.Level];
            Component.Container.LevelIndex.text = level.Level.ToString();
            gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.DrawElements);
            ShowRequiredScore(gameSystemsHandler);
        }

        public void DisposeMechanic()
        {
        }
    }
}
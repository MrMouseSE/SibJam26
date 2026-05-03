using GameScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.ScoreViewScripts.LevelViewScripts
{
    public class LevelViewSystem : IGameSystem
    {
        public LevelViewComponent Component;
        public LevelViewMechanic Mechanic;

        public LevelViewSystem()
        {
            Component = new LevelViewComponent();
            Mechanic = new LevelViewMechanic(Component);
        }
        
        public void Initialize(GameSystemsHandler gameSystemsHandler)
        {
        }

        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.UpdateLevelView) return;
            Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
        }
    }
}
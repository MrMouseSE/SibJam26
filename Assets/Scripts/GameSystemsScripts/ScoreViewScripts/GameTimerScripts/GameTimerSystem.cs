using GameScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.ScoreViewScripts.GameTimerScripts
{
    public class GameTimerSystem : IGameSystem
    {
        public GameTimerComponent Component;
        public GameTimerMechanic Mechanic;

        public GameTimerSystem()
        {
            Component = new GameTimerComponent();
            Mechanic = new GameTimerMechanic(Component);
        }

        public void Initialize(GameSystemsHandler gameSystemsHandler)
        {
        }

        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (gameSystemsHandler.StateSystem.Component.CurrentGameState == GameStates.StartGame) return;
            Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
        }
    }
}

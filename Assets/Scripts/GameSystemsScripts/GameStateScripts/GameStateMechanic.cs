using GameScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.GameStateScripts
{
    public class GameStateMechanic : IGameMechanic
    {
        public GameStateComponent Component;

        public GameStateMechanic(GameStateComponent component)
        {
            Component = component;
        }
        
        public void SetStartState()
        {
            ChangeState(GameStates.StartGame);
        }

        public void ChangeState(GameStates newState)
        {
            Component.PreviousGameState = Component.CurrentGameState;
            Component.CurrentGameState = newState;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            
        }

        public void DisposeMechanic()
        {
        }
    }
}
using ScenesOperatingScripts;

namespace GameSystemsScripts.LevelsSystemScripts.GameCompleteSystemScripts
{
    public class GameCompleteMechanic : IGameMechanic
    {
        public GameCompleteComponent Component;

        public GameCompleteMechanic(GameCompleteComponent component)
        {
            Component = component;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (Component.IsGameComplete != true) return;
            
            
            //TODO: GAME COMPLETE HERE (restart or something)
        }

        public void DisposeMechanic()
        {
        }
    }
}
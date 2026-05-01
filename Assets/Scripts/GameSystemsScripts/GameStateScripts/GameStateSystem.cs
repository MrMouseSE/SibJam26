using ScenesOperatingScripts;

namespace GameSystemsScripts.GameStateScripts
{
    public class GameStateSystem : IGameSystem
    {
        public GameStateComponent Component;
        public GameStateMechanic Mechanic;

        public GameStateSystem()
        {
            Component = new GameStateComponent();
            Mechanic = new GameStateMechanic(Component);
        }
        
        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
        }
    }
}
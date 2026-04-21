using ScenesOperatingScripts;

namespace MainMenuScripts.StartGameSystemScripts
{
    public class StartGameSystem : IGameSystem
    {
        public StartGameMechanic Mechanic;
        public StartGameComponent Component;

        public StartGameSystem(MenuButtonContainer container)
        {
            Component = new StartGameComponent(container);
            Mechanic = new StartGameMechanic(Component);
        }
    
        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (Component.IsActionLock) return;
            if (Component.IsStartGameButtonPushed) Mechanic.StartGame();
        }

        public void DisposeSystem()
        {
            Mechanic.DisposeMechanic();
        }
    }
}
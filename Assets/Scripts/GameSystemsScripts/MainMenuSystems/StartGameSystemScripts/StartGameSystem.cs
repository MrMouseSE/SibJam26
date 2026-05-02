using GameScripts;
using MainMenuScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.MainMenuSystems.StartGameSystemScripts
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

        public void Initialize(GameSystemsHandler gameSystemsHandler)
        {
        }

        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (Component.IsActionLock) return;
            if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.StartGame) return;
            if (Component.IsStartGameButtonPushed) Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
            Mechanic.DisposeMechanic();
        }
    }
}
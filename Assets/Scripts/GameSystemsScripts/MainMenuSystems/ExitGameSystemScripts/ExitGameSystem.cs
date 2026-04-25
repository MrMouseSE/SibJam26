using MainMenuScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.MainMenuSystems.ExitGameSystemScripts
{
    public class ExitGameSystem : IGameSystem
    {
        public ExitGameComponent Component;
        public ExitGameMechanic Mechanic;

        public ExitGameSystem(MenuButtonContainer container)
        {
            Component = new ExitGameComponent(container);
            Mechanic = new ExitGameMechanic(Component);
        }

        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (Component.IsGameExitProcess)
            {
                gameSystemsHandler.DisposeSystems();
            }
        }

        public void DisposeSystem()
        {
            Mechanic.DisposeMechanic();
        }
    }
}
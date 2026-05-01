using MainMenuScripts;

namespace GameSystemsScripts.MainMenuSystems.ExitGameSystemScripts
{
    public class ExitGameComponent
    {
        public MenuButtonContainer Container;
        public ExitGameComponent(MenuButtonContainer container)
        {
            Container = container;
        }

        public bool IsGameExitProcess;
    }
}
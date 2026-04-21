namespace MainMenuScripts.ExitGameSystemScripts
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
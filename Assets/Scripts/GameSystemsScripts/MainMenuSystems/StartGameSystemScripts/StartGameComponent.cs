using MainMenuScripts;

namespace GameSystemsScripts.MainMenuSystems.StartGameSystemScripts
{
    public class StartGameComponent
    {
        public MenuButtonContainer ButtonContainer;
        public StartGameComponent(MenuButtonContainer buttonContainer)
        {
            ButtonContainer = buttonContainer;
        }
        
        public bool IsActionLock;
        public bool IsStartGameButtonPushed;
    }
}
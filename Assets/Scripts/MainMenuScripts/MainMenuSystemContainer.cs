using MainMenuScripts.ExitGameSystemScripts;
using MainMenuScripts.StartGameSystemScripts;
using ScenesOperatingScripts;

namespace MainMenuScripts
{
    public class MainMenuSystemContainer : SceneSystemsContainer
    {
        public MenuButtonContainer StartButtonContainer;
        public MenuButtonContainer ExitButtonContainer;

        public override void InitializeSceneSystems(GameSystemsHandler systemsHandler)
        {
            systemsHandler.AddGameSystem(new StartGameSystem(StartButtonContainer));
            systemsHandler.AddGameSystem(new ExitGameSystem(ExitButtonContainer));
        }
    }
}

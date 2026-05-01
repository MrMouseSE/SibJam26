using ScenesOperatingScripts;

namespace GameSystemsScripts.GameSpeedScripts
{
    public class GameSpeedMechanic : IGameMechanic
    {
        public GameSpeedComponent Component;

        public GameSpeedMechanic(GameSpeedComponent component)
        {
            Component = component;
        }


        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            
        }

        public void DisposeMechanic()
        {
            
        }
    }
}
using AnimationDescriptionsScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.GameSpeedScripts
{
    public class GameSpeedSystem :IGameSystem
    {
        public GameSpeedComponent Component;
        public GameSpeedMechanic Mechanic;

        public GameSpeedSystem(AnimationsDescription animationsDescription)
        {
            Component = new GameSpeedComponent(animationsDescription);
            Mechanic = new GameSpeedMechanic(Component);
        }
        
        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
        }

        public void DisposeSystem()
        {
        }
    }
}
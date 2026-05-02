using ScenesOperatingScripts;
using TweenScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.BoilingSystemScripts.CompleteBoilingScripts
{
    public class CompleteBoilingSystem : IGameSystem
    {
        public CompleteBoilingComponent Component;
        public CompleteBoilingMechanic Mechanic;

        public CompleteBoilingSystem()
        {
            Component = new CompleteBoilingComponent();
            Mechanic = new CompleteBoilingMechanic(Component);
        }
        
        public void Initialize(GameSystemsHandler gameSystemsHandler)
        {
            
        }

        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
        }

        public void DisposeSystem()
        {
        }
    }
}
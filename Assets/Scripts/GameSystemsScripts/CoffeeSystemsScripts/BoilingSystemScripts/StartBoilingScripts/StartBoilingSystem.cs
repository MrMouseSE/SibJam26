using GameScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.BoilingSystemScripts.StartBoilingScripts
{
    public class StartBoilingSystem :IGameSystem
    {
        public StartBoilingComponent Component;
        public StartBoilingMechanic Mechanic;

        public StartBoilingSystem()
        {
            Component = new StartBoilingComponent();
            Mechanic = new StartBoilingMechanic(Component);
        }
        
        public void Initialize(GameSystemsHandler gameSystemsHandler)
        {
        }

        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.StartBoil) return;
            Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
        }
    }
}
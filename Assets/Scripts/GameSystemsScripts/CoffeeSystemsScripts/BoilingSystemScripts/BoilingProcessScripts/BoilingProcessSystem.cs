using AnimationDescriptionsScripts;
using GameScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.BoilingSystemScripts.BoilingProcessScripts
{
    public class BoilingProcessSystem : IGameSystem
    {
        public BoilingProcessComponent Component;
        public BoilingProcessMechanic Mechanic;

        public BoilingProcessSystem(BoilDescription description)
        {
            Component = new BoilingProcessComponent(description);
            Mechanic = new BoilingProcessMechanic(Component);
        }

        public void Initialize(GameSystemsHandler gameSystemsHandler)
        {
            Mechanic.InitializeBoilAnimationValues(gameSystemsHandler);
        }

        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.BoilingCoffee) return;
            Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
        }
    }
}
using GameScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.BoostersSystemScripts
{
    public class BoostersSystem : IGameSystem
    {
        public BoostersComponent Component;
        public BoostersMechanic Mechanic;

        public BoostersSystem(BoostersDescription boostersDescription)
        {
            Component = new BoostersComponent(boostersDescription);
            Mechanic = new BoostersMechanic(Component);
        }

        public void Initialize(GameSystemsHandler gameSystemsHandler)
        {
            Mechanic.InitializeBoosters();
        }

        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.CalculateValue) return;
            Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
        }
    }
}

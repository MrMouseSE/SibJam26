using ScenesOperatingScripts;

namespace GameSystemsScripts.GameInputSystem
{
    public class GameInputSystem : IGameSystem
    {
        public GameInputMechanic Mechanic;
        public GameInputComponent Component;

        public GameInputSystem()
        {
            Component = new GameInputComponent();
            Mechanic = new GameInputMechanic(Component);
            
        }

        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (Component.IsInputLocked) return;
            Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
            Mechanic.DisposeMechanic();
        }
    }
}
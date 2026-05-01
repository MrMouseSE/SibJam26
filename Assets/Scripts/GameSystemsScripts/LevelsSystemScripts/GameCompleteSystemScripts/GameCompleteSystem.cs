using ScenesOperatingScripts;

namespace GameSystemsScripts.LevelsSystemScripts.GameCompleteSystemScripts
{
    public class GameCompleteSystem : IGameSystem
    {
        public GameCompleteComponent Component;
        public GameCompleteMechanic Mechanic;

        public GameCompleteSystem()
        {
            Component = new GameCompleteComponent();
            Mechanic = new GameCompleteMechanic(Component);
        }
        
        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
        }
    }
}
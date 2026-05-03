using GameScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.ScoreViewScripts.InterfaceScoreScripts
{
    public class InterfaceScoreSystem : IGameSystem
    {
        public InterfaceScoreComponent Component;
        public InterfaceScoreMechanic Mechanic;

        public InterfaceScoreSystem()
        {
            Component = new InterfaceScoreComponent();
            Mechanic = new InterfaceScoreMechanic(Component);
        }
        
        public void Initialize(GameSystemsHandler gameSystemsHandler)
        {
            
        }

        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (gameSystemsHandler.StateSystem.Component.CurrentGameState == GameStates.StartGame) return;
            Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
        }
    }
}
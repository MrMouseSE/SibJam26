using GameScripts;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.SelectionButtonsScripts;
using ScenesOperatingScripts;
using TweenScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsDrawScripts
{
    public class ElementsDrawMechanic : IGameMechanic
    {
        public ElementsDrawComponent Component;
        
        private readonly GameSystemsHandler _gameSystemsHandler;

        public ElementsDrawMechanic(ElementsDrawComponent component, GameSystemsHandler gameSystemsHandler)
        {
            _gameSystemsHandler = gameSystemsHandler;
            Component = component;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            //TODO: Draw action

            var selectionButtonSystem = (SelectionButtonsSystem)gameSystemsHandler.GetGameSystem(typeof(SelectionButtonsSystem));
            selectionButtonSystem.Component.IsSelectionStateStartedThisFrame = true;
            
            gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.AwaitAnimation);
        }

        public void OnDrawAnimationFinished(UniTaskAnimationObject animationObject)
        {
            _gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.SelectElements);
        }

        public void DisposeMechanic()
        {
        }
    }
}
using System.Threading;
using GameScripts;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsHandSystemScripts;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.SelectionButtonsScripts;
using GameSystemsScripts.GameSpeedScripts;
using ScenesOperatingScripts;
using SupportScripts;
using TweenScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsDrawScripts
{
    public class ElementsDrawMechanic : IGameMechanic
    {
        public ElementsDrawComponent Component;
        
        private GameSystemsHandler _gameSystemsHandler;

        public ElementsDrawMechanic(ElementsDrawComponent component)
        {
            Component = component;
        }
        
        public void SetHandler(GameSystemsHandler gameSystemsHandler)
        {
            _gameSystemsHandler = gameSystemsHandler;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            var handSystem = (ElementsHandSystem)gameSystemsHandler.GetGameSystem(typeof(ElementsHandSystem));
            int currentElementToDrawCount = handSystem.Component.CurrentElementsToDrawCapacity;
            
            Component.DrawedElements.Clear();
            for (int i = 0; i < currentElementToDrawCount; i++)
            {
                var elementContainer = Component.Container.ElementContainers[i];
                elementContainer.ElementDisappearAnimation.SetForceState(true);
                StaticElementFactory.SetValuesToContainer(elementContainer);
                Component.DrawedElements.Add(elementContainer);
            }
            
            Component.Container.AppearAnimation.SetForceState(true);
            
            GameSpeedSystem speedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
            CancellationTokenSource cts = new CancellationTokenSource();
            UniTaskAnimationLazyObject animObj = new UniTaskAnimationLazyObject(Component.Container.AppearAnimation, ref cts,
                speedSystem.Component.AnimationsDescription.ElementsAnimationDescription.ElementDrawAnimationDuration, true);
            animObj.Play().Forget();
            animObj.AnimationCompleted += OnDrawAnimationFinished;
            
            var selectionButtonSystem = (SelectionButtonsSystem)gameSystemsHandler.GetGameSystem(typeof(SelectionButtonsSystem));
            selectionButtonSystem.Component.IsSelectionStateStartedThisFrame = true;
            
            gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.AwaitAnimation);
        }

        public void OnDrawAnimationFinished(UniTaskAnimationLazyObject animationObject)
        {
            animationObject.AnimationCompleted -= OnDrawAnimationFinished;
            _gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.SelectElements);
        }

        public void DisposeMechanic()
        {
        }

    }
}
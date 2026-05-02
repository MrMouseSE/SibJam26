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
            var speedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
            var selectionButtonSystem = (SelectionButtonsSystem)gameSystemsHandler.GetGameSystem(typeof(SelectionButtonsSystem));
            
            int currentElementToDrawCount = handSystem.Component.CurrentElementsToDrawCapacity;
            
            Component.DrawedElements.Clear();
            for (var index = 0; index < Component.Container.ElementContainers.Count; index++)
            {
                var container = Component.Container.ElementContainers[index];
                bool isUsed = index < currentElementToDrawCount;
                container.ElementSelectAnimation.SetForceState(true);
                container.ElementDisappearAnimation.SetForceState(isUsed);
                container.IsSelected = false;
                container.IsUsedInGame = isUsed;
                container.ElementCollider.enabled = isUsed;
                container.AnimationsDescription = speedSystem.Component.AnimationsDescription;
                StaticElementFactory.SetValuesToContainer(container);
                if (isUsed)
                    Component.DrawedElements.Add(container);
            }
            
            Component.Container.AppearAnimation.SetForceState(true);
            
            CancellationTokenSource cts = new CancellationTokenSource();
            UniTaskAnimationLazyObject animObj = new (Component.Container.AppearAnimation, ref cts,
                speedSystem.Component.AnimationsDescription.ElementsAnimationDescription.ElementDrawAnimationDuration, true);
            animObj.Play().Forget();
            animObj.AnimationCompleted += OnDrawAnimationFinished;
            
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
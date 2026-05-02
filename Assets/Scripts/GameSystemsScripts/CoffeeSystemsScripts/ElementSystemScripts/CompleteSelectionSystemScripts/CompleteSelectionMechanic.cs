using System.Threading;
using GameScripts;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsDrawScripts;
using GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.FillRecipeScripts;
using GameSystemsScripts.GameSpeedScripts;
using ScenesOperatingScripts;
using TweenScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.CompleteSelectionSystemScripts
{
    public class CompleteSelectionMechanic : IGameMechanic
    {
        public CompleteSelectionComponent Component;
        
        private GameSystemsHandler _gameSystemsHandler;

        public CompleteSelectionMechanic(CompleteSelectionComponent component, GameSystemsHandler gameSystemsHandler)
        {
            _gameSystemsHandler = gameSystemsHandler;
            Component = component;
            Component.Container.OnButtonPressed += OnSelectionComplete;
        }

        private void OnSelectionComplete()
        {
            Component.IsButtonPressedThisFrame = false;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            FillRecipeSystem fillSystem = (FillRecipeSystem)gameSystemsHandler.GetGameSystem(typeof(FillRecipeSystem));
            if (fillSystem.Component.RecipeContainers.Count < 1) return;
            if (!Component.IsButtonPressedThisFrame) return;
            
            var animationObject = new UniTaskAnimationObject();
            var cancellationTokenSource = new CancellationTokenSource();
            var speedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
            animationObject.StartAnimation(Component.Container.ButtonAnimations, 
                cancellationTokenSource.Token, speedSystem.Component.AnimationsDescription.CompleteSelectionButtonAnimationDuration, false).Forget();
            Component.IsButtonPressedThisFrame = false;
            animationObject.AnimationCompleted += OnCompleteButtonAnimationFinished;
            _gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.AwaitAnimation);
            
            //TODO: FillRecipeComponent to animate Selected containers ?????? mb same animation like disappear
            /*var fillRecipeSystem = (FillRecipeSystem)gameSystemsHandler.GetGameSystem(typeof(FillRecipeSystem));
            foreach (var recipeContainer in fillRecipeSystem.Component.RecipeContainers)
            {
                recipeContainer.ElementAppearAnimation
            }*/
            //TODO: FillRecipeComponent to animate Selected containers
            
            
            var drawSystem = (ElementsDrawSystem)gameSystemsHandler.GetGameSystem(typeof(ElementsDrawSystem));
            foreach (var container in drawSystem.Component.Container.ElementContainers)
            {
                container.IsUsedInGame = false;
                float duration = container.IsSelected
                    ? speedSystem.Component.AnimationsDescription.ElementsAnimationDescription.ElementSelectedDisappearDuration :
                    speedSystem.Component.AnimationsDescription.ElementsAnimationDescription.ElementUnselectedDisappearDuration;
                UniTaskAnimationObject containerAnimation = new UniTaskAnimationObject();
                CancellationTokenSource cts = new CancellationTokenSource();
                
                containerAnimation.StartAnimation(container.ElementDisappearAnimation, cts.Token, duration, false).Forget();
            }
        }

        public void DisposeMechanic()
        {
            Component.Container.OnButtonPressed -= OnSelectionComplete;
        }

        private void OnCompleteButtonAnimationFinished(UniTaskAnimationObject uniTaskAnimationObject)
        {
            uniTaskAnimationObject.AnimationCompleted -= OnCompleteButtonAnimationFinished;
            _gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.CompareRecipe);
        }
    }
}
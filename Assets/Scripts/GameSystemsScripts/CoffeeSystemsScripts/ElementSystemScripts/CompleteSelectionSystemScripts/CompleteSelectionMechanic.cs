using System.Threading;
using GameScripts;
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
            if (!Component.IsButtonPressedThisFrame) return;
            
            var animationObject = new UniTaskAnimationObject();
            var cancellationTokenSource = new CancellationTokenSource();
            var speedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
            animationObject.StartAnimation(Component.Container.ButtonAnimations, 
                cancellationTokenSource.Token, speedSystem.Component.AnimationsDescription.CompleteSelectionButtonAnimationDuration, false).Forget();
            Component.IsButtonPressedThisFrame = false;
            animationObject.AnimationCompleted += OnCompleteButtonAnimationFinished;
            _gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.AwaitAnimation);
            
            //TODO: FillRecipeComponent to animate Selected containers
            /*var fillRecipeSystem = (FillRecipeSystem)gameSystemsHandler.GetGameSystem(typeof(FillRecipeSystem));
            foreach (var recipeContainer in fillRecipeSystem.Component.RecipeContainers)
            {
                recipeContainer.ElementAppearAnimation
            }*/
            //TODO: FillRecipeComponent to animate Selected containers
            
            
            //TODO: ElementsDrawComponent to animate Unselected containers;
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
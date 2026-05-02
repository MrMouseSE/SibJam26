using System.Threading;
using GameScripts;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsDrawScripts;
using GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.FillRecipeScripts;
using GameSystemsScripts.GameSpeedScripts;
using ScenesOperatingScripts;
using SoundsComponentsScripts;
using TweenScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.CompleteSelectionSystemScripts
{
    public class CompleteSelectionMechanic : IGameMechanic
    {
        public CompleteSelectionComponent Component;
        
        private GameSystemsHandler _gameSystemsHandler;

        public CompleteSelectionMechanic(CompleteSelectionComponent component)
        {
            Component = component;
        }

        public void SetButtonContainer(GameButtonContainer buttonContainer)
        {
            Component.CompleteButtonContainer = buttonContainer;
            Component.CompleteButtonContainer.OnButtonPressed += OnSelectionComplete;
        }

        public void SetSystemHandler(GameSystemsHandler handler)
        {
            _gameSystemsHandler = handler;
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
            
            var speedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
            var cancelToken = new CancellationTokenSource();
            var animationObject = new UniTaskAnimationLazyObject(Component.CompleteButtonContainer.ButtonActivateAnimations, 
                ref cancelToken, speedSystem.Component.AnimationsDescription.ActivateAnimationDuration, false);
            animationObject.Play().Forget();
            animationObject.AnimationCompleted += OnCompleteButtonAnimationFinished;
            
            Component.IsButtonPressedThisFrame = false;
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
                CancellationTokenSource cts = new CancellationTokenSource();
                UniTaskAnimationLazyObject animObj = new UniTaskAnimationLazyObject(container.ElementDisappearAnimation, ref cts, duration, false);
                animObj.Play().Forget();
                container.SoundContainer.Play(SoundType.DeathSound);
            }
        }

        public void DisposeMechanic()
        {
            Component.CompleteButtonContainer.OnButtonPressed -= OnSelectionComplete;
        }

        private void OnCompleteButtonAnimationFinished(UniTaskAnimationLazyObject uniTaskAnimationObject)
        {
            uniTaskAnimationObject.AnimationCompleted -= OnCompleteButtonAnimationFinished;
            _gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.CompareRecipe);
        }
    }
}
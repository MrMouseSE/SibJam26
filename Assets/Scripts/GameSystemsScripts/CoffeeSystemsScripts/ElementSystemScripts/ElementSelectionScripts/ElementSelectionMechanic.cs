using System.Threading;
using GameSystemsScripts.GameSpeedScripts;
using ScenesOperatingScripts;
using SoundsComponentsScripts;
using TweenScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts
{
    public class ElementSelectionMechanic : IGameMechanic
    {
        public readonly ElementSelectionComponent SelectionComponent;

        public ElementSelectionMechanic(ElementSelectionComponent selectionComponent)
        {
            SelectionComponent = selectionComponent;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (SelectionComponent.CurrentTouchedContainer == null) return;
            SelectionComponent.CurrentTouchedContainer.SoundContainer.Play(SoundType.ActionSound);
            SelectionComponent.CurrentTouchedContainer.AnimationCancellationToken.Cancel();
            SelectionComponent.CurrentTouchedContainer.AnimationCancellationToken = new CancellationTokenSource();
            UniTaskAnimationObject animationObject = new UniTaskAnimationObject();
            var gameSpeedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
            float elementAnimationDuration = gameSpeedSystem.Component.AnimationsDescription
                .ElementsAnimationDescription.ElementSelectionAnimationDuration;
            animationObject.StartAnimation(SelectionComponent.CurrentTouchedContainer, elementAnimationDuration).Forget();
        }

        public void DisposeMechanic()
        {
        }
    }
}
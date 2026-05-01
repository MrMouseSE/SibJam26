using System.Threading;
using GameSystemsScripts.GameSpeedScripts;
using ScenesOperatingScripts;
using SoundsComponentsScripts;
using TweenScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementSelectionScripts
{
    public class ElementSelectionMechanic : IGameMechanic
    {
        public ElementSelectionComponent Component;

        public ElementSelectionMechanic(ElementSelectionComponent component)
        {
            Component = component;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (Component.CurrentTouchedContainer == null) return;
            
            Component.CurrentTouchedContainer.SoundContainer.Play(SoundType.ActionSound);
            
            Component.CurrentTouchedContainer.AnimationCancellationToken.Cancel();
            Component.CurrentTouchedContainer.AnimationCancellationToken = new CancellationTokenSource();
            
            UniTaskAnimationObject animationObject = new UniTaskAnimationObject();
            var gameSpeedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
            float elementAnimationDuration = gameSpeedSystem.Component.AnimationsDescription
                .ElementsAnimationDescription.ElementSelectionAnimationDuration;
            animationObject.StartAnimation(Component.CurrentTouchedContainer, elementAnimationDuration).Forget();
        }

        public void DisposeMechanic()
        {
        }
    }
}
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
            var container = Component.CurrentTouchedContainer;
            container.SoundContainer.Play(SoundType.ActionSound);
            
            var gameSpeedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
            float duration = gameSpeedSystem.Component.AnimationsDescription.ElementsAnimationDescription.ElementSelectionAnimationDuration;
            UniTaskAnimationLazyObject animObj = new(container.ElementSelectAnimation,
                ref container.CancelToken, duration, !container.IsSelected);
            
            animObj.Play().Forget();
            container.IsSelected = !container.IsSelected;
        }

        public void DisposeMechanic()
        {
        }
    }
}
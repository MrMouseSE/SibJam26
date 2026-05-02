using System.Linq;
using AnimationDescriptionsScripts;
using ScenesOperatingScripts;
using TweenScripts;
using UnityEngine;

namespace GameSystemsScripts.CameraSystem
{
    public class GameCameraMechanic : IGameMechanic
    {
        public GameCameraComponent Component;

        public GameCameraMechanic(GameCameraComponent component)
        {
            Component = component;
        }

        public void AnimateCameraMovement(Transform from, Transform to, CameraAnimationDescription cameraAnimation)
        {
            var tween = (TweenGroupAnimation)Component.CurrentCameraHolder.CameraMoverTween;
            tween.Duration = cameraAnimation.CameraAnimationDuration;
            
            var positionTween = (PositionTweenAnimation)tween.Animations.ToList().Find(x => x.GetType() == typeof(PositionTweenAnimation));
            positionTween.FromPosition = from.position;
            positionTween.ToPosition = to.position;
            positionTween.AnimationRule = cameraAnimation.CameraAnimationCurve;
            var rotationTween = (RotateTweenAnimation)tween.Animations.ToList().Find(x => x.GetType() == typeof(RotateTweenAnimation));
            rotationTween.FromRotation = from.rotation.eulerAngles;
            rotationTween.ToRotation = to.rotation.eulerAngles;
            rotationTween.AnimationRule = cameraAnimation.CameraAnimationCurve;
            
            UniTaskAnimationLazyObject animObj = new(tween, ref Component.CurrentCameraHolder.CameraMoverCancellationToken, 
                cameraAnimation.CameraAnimationDuration, true);
            animObj.Play().Forget();
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            Component.CurrentCamera.CameraTransform.position = Component.CurrentCameraHolder.CameraRoot.position;
            Component.CurrentCamera.CameraTransform.rotation = Component.CurrentCameraHolder.CameraRoot.rotation;
        }
        
        public void DisposeMechanic()
        {
        }
    }
}
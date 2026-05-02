using AnimationDescriptionsScripts;
using CoffeeScripts;
using GameSystemsScripts.CameraSystem;
using GameSystemsScripts.CoffeeSystemsScripts.BoilingSystemScripts.CompleteBoilingScripts;
using GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.RecipeHandlerScripts;
using GameSystemsScripts.GameSpeedScripts;
using ScenesOperatingScripts;
using UnityEngine;

namespace GameSystemsScripts.CoffeeSystemsScripts.BoilingSystemScripts.BoilingProcessScripts
{
    public class BoilingProcessMechanic : IGameMechanic
    {
        public BoilingProcessComponent Component;

        public BoilingProcessMechanic(BoilingProcessComponent component)
        {
            Component = component;
        }

        public void InitializeBoilAnimationValues(GameSystemsHandler gameSystemsHandler)
        {
            Component.Container.ProcessBoilTweenGroup.Duration = Component.BoilDescription.BoilProcessAnimationDuration;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            CompleteBoilingSystem compeleBoilingSystem = (CompleteBoilingSystem)gameSystemsHandler.GetGameSystem(typeof(CompleteBoilingSystem));
            GameSpeedSystem speedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
            var boilingDescription = speedSystem.Component.AnimationsDescription.Boil;
            var container = Component.Container;
            if (compeleBoilingSystem.Component.IsBoilingComplete)
            {
                container.ProcessCancellationToken.Cancel();
                container.IndicatorDisplaySpriteRenderer.color = Color.white;
                container.IndicatorArrowSpriteRenderer.color = Color.white;
                container.IndicatorArrowTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
                container.IndicatorShakeTweenGroup.AnimateByUpdate = false;
                container.ProcessBoilTweenGroup.AnimateByUpdate = false;
            
                GameCameraSystem cameraSystem = (GameCameraSystem)gameSystemsHandler.GetGameSystem(typeof(GameCameraSystem));
                cameraSystem.Mechanic.AnimateCameraMovement(container.BoildFocusCameraPoint, container.NormalCameraPoint, 
                    speedSystem.Component.AnimationsDescription.Camera);
                Component.BoilMultiplier = boilingDescription.BoilMultiplyerCurve.Evaluate(Component.BoilValue/ boilingDescription.BoilExtreemeValue);
                return;
            }
            
            Component.BoilingTime += deltaTime;
            Component.BoilValue += deltaTime * Random.Range(boilingDescription.BoilAddRangeMultiplier.x, boilingDescription.BoilAddRangeMultiplier.y);
            SetIndicatorValues(boilingDescription);
        }

        private void SetIndicatorValues(BoilDescription description)
        {
            var container = Component.Container;
            var normalizeBoilValue = Component.BoilValue / description.BoilExtreemeValue;
            var currentColor = Color.Lerp(description.NormalColor, description.ExtreemeColor, normalizeBoilValue);
            container.IndicatorDisplaySpriteRenderer.color = currentColor;
            container.IndicatorArrowSpriteRenderer.color = currentColor;
            var currentAngle = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(description.ArrowExtreemeAngle), normalizeBoilValue);
            container.IndicatorArrowTransform.localRotation = currentAngle;
        }

        public void DisposeMechanic()
        {
        }
    }
}
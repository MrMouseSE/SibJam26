using AnimationDescriptionsScripts;
using CoffeeScripts;
using GameSystemsScripts.CameraSystem;
using GameSystemsScripts.CoffeeSystemsScripts.BoilingSystemScripts.CompleteBoilingScripts;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.CompleteSelectionSystemScripts;
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

        public void SetButtonContainer(BoilCoffeeContainer container)
        {
            Component.Container = container;
            Component.Container.ProcessBoilTweenGroup.Duration = Component.BoilDescription.BoilProcessAnimationDuration;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            CompleteBoilingSystem compeleBoilingSystem = (CompleteBoilingSystem)gameSystemsHandler.GetGameSystem(typeof(CompleteBoilingSystem));
            GameSpeedSystem speedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
            var boilingDescription = speedSystem.Component.AnimationsDescription.Boil;
            var container = Component.Container;
            if (Component.BoilValue > boilingDescription.BoilExtreemeValue)
            {
                compeleBoilingSystem.Component.IsBoilingComplete = true;
            }
            if (compeleBoilingSystem.Component.IsBoilingComplete)
            {
                container.ProcessCancellationToken.Cancel();
                container.IndicatorDisplaySpriteRenderer.color = Color.white;
                container.IndicatorArrowSpriteRenderer.color = Color.white;
                container.IndicatorArrowTransform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                container.IndicatorShakeTweenGroup.AnimateByUpdate = false;
                container.ProcessBoilTweenGroup.AnimateByUpdate = false;
                compeleBoilingSystem.Component.CompleteBoilingButtonContainer.ExtremeButtonSprite.color = new Color(1f, 1f, 1f, 0f);
            
                GameCameraSystem cameraSystem = (GameCameraSystem)gameSystemsHandler.GetGameSystem(typeof(GameCameraSystem));
                cameraSystem.Mechanic.AnimateCameraMovement(container.BoildFocusCameraPoint, container.NormalCameraPoint, 
                    speedSystem.Component.AnimationsDescription.Camera);
                var normalizeBoilValue = Component.BoilValue/ boilingDescription.BoilExtreemeValue;
                Component.BoilMultiplier = boilingDescription.BoilMultiplyerCurve.Evaluate(normalizeBoilValue) * boilingDescription.MultValue;
                return;
            }
            
            Component.BoilingTime += deltaTime;
            Component.BoilValue += deltaTime * Random.Range(boilingDescription.BoilAddRangeMultiplier.x, boilingDescription.BoilAddRangeMultiplier.y);
            
            SetIndicatorValues(boilingDescription, compeleBoilingSystem.Component.CompleteBoilingButtonContainer.ExtremeButtonSprite);
        }

        private void SetIndicatorValues(BoilDescription description, SpriteRenderer extremeButtonSprite)
        {
            var container = Component.Container;
            var normalizeBoilValue = Component.BoilValue / description.BoilExtreemeValue;
            var currentColor = Color.Lerp(description.NormalColor, description.ExtreemeColor, normalizeBoilValue);
            container.IndicatorDisplaySpriteRenderer.color = currentColor;
            container.IndicatorArrowSpriteRenderer.color = currentColor;
            var currentAngle = Vector3.Lerp(Vector3.zero, description.ArrowExtreemeAngle, normalizeBoilValue);
            container.IndicatorArrowTransform.localRotation = Quaternion.Euler(currentAngle);
            extremeButtonSprite.color = new Color(1f, 1f, 1f, normalizeBoilValue);
        }

        public void DisposeMechanic()
        {
        }
    }
}
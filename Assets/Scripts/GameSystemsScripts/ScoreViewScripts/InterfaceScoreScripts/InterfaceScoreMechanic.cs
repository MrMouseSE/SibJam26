using CoffeeScripts;
using CoffeeScripts.ElementsInventoryScripts;
using GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.RecipeHandlerScripts;
using GameSystemsScripts.GameSpeedScripts;
using GameSystemsScripts.LevelsSystemScripts.LevelHandlerScripts;
using ScenesOperatingScripts;
using TweenScripts;
using UnityEngine;

namespace GameSystemsScripts.ScoreViewScripts.InterfaceScoreScripts
{
    public class InterfaceScoreMechanic : IGameMechanic
    {
        public InterfaceScoreComponent Component;

        public InterfaceScoreMechanic(InterfaceScoreComponent component)
        {
            Component = component;
        }

        public void SetContainer(InterfaceScoreContainer interfaceScoreContainer, GameSystemsHandler gameSystemsHandler)
        {
            Component.Container = interfaceScoreContainer;
            Component.Container.MaximumScoreText.ScoreText.text = PlayerPrefs.GetString("MaxScore");
            Component.Container.PreviousScoreText.ScoreText.text = PlayerPrefs.GetString("PreviousScore");
            ShowRequiredScore(gameSystemsHandler);
        }
        
        public void ShowRequiredScore(GameSystemsHandler gameSystemsHandler)
        {
            var levelHandler = (LevelHandlerSystem)gameSystemsHandler.GetGameSystem(typeof(LevelHandlerSystem));
            var speedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
            
            var score = 
                levelHandler.Component.DaysDescription.DaysAchievementsDescriptions[levelHandler.Component.Day].
                    LevelsAchievements[levelHandler.Component.Level].LevelScoreToAchieve;

            var containerRequireScore = Component.Container.RequireScore;
            
            containerRequireScore.SetScoreToAnimation(score);
            UniTaskAnimationLazyObject animMaxObj = new(containerRequireScore.ScoreAnimation,
                ref containerRequireScore.CancToken, speedSystem.Component.AnimationsDescription.ScoreAnimationDuration, true);
            animMaxObj.Play().Forget();
        }
        
        public void UpdateScore(float resultValue, float duration)
        {
            var container = Component.Container;
            if (Component.MaximumReachedScore < resultValue)
            {
                Component.MaximumReachedScore = resultValue;
                
                container.MaximumScoreText.SetScoreToAnimation(resultValue);
                UniTaskAnimationLazyObject animMaxObj = new(container.MaximumScoreText.ScoreAnimation,
                    ref container.MaximumScoreText.CancToken, duration, true);
                animMaxObj.Play().Forget();
            }
                
            Component.PreviousReachedScore = Component.CurrentReachedScore;
            Component.CurrentReachedScore = resultValue;

            container.PreviousScoreText.SetScoreToAnimation(resultValue);
            UniTaskAnimationLazyObject animObj = new(container.PreviousScoreText.ScoreAnimation,
                ref container.PreviousScoreText.CancToken, duration, true);
            animObj.Play().Forget();
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            RecipeHandlerSystem handlerSystem = (RecipeHandlerSystem)gameSystemsHandler.GetGameSystem(typeof(RecipeHandlerSystem));
            CoffeeDescription coffeeDescription = handlerSystem.Component.CurrentCoffeeDescription;
            int index = 0;
            foreach (var element in ElementsStaticInventory.CurrentAvailableElements)
            {
                var elementContainer = Component.Container.CurrentElements[index];
                elementContainer.ElementRenderer.enabled = element.Value > 0;
                var emenetDesc = coffeeDescription.Elements.Find(x=>x.ElementName == element.Key);
                elementContainer.ElementSprite = emenetDesc.Sprite;
                elementContainer.ElementRenderer.sprite = emenetDesc.Sprite;
                elementContainer.CountText.enabled = element.Value > 0;
                elementContainer.CountText.text = element.Value.ToString();
                elementContainer.BackRenderer.enabled = element.Value > 0;
                elementContainer.BackRenderer.color = emenetDesc.BackColor;
                index++;
            }
        }

        public void DisposeMechanic()
        {
        }
    }
}
using CoffeeScripts;
using CoffeeScripts.ElementsInventoryScripts;
using GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.RecipeHandlerScripts;
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

        public void SetContainer(InterfaceScoreContainer interfaceScoreContainer)
        {
            Component.Container = interfaceScoreContainer;
            Component.Container.MaximumScoreText.ScoreText.text = PlayerPrefs.GetString("MaxScore");
            Component.Container.PreviousScoreText.ScoreText.text = PlayerPrefs.GetString("PreviousScore");
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
                
            Component.PreviousReachedScore = Component.CurrestReachedScore;
            Component.CurrestReachedScore = resultValue;

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
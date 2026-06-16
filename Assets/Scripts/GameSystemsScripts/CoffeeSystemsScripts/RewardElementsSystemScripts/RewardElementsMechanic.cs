using CoffeeScripts.ElementsInventoryScripts;
using GameScripts;
using GameSystemsScripts.CoffeeSystemsScripts.BoostersSystemScripts;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsHandSystemScripts;
using GameSystemsScripts.EconomyScripts;
using GameSystemsScripts.LevelsSystemScripts.LevelCompleteScripts;
using GameSystemsScripts.LevelsSystemScripts.LevelHandlerScripts;
using ScenesOperatingScripts;
using UnityEngine;

namespace GameSystemsScripts.CoffeeSystemsScripts.RewardElementsSystemScripts
{
    public class RewardElementsMechanic : IGameMechanic
    {
        public RewardElementsComponent Component;

        public RewardElementsMechanic(RewardElementsComponent component)
        {
            Component = component;
        }

        public void SetContainer(RewardShopContainer container)
        {
            if (Component.RewardShopContainer != null)
            {
                Component.RewardShopContainer.OnBuyBooster -= RequestBoosterBuy;
                Component.RewardShopContainer.OnBuyIngredients -= RequestIngredientBuy;
                Component.RewardShopContainer.OnBuySlot -= RequestSlotBuy;
                Component.RewardShopContainer.OnContinue -= Continue;
                Component.RewardShopContainer.UnbindButtons();
            }

            Component.RewardShopContainer = container;
            if (Component.RewardShopContainer == null)
            {
                return;
            }

            Component.RewardShopContainer.BindButtons();
            Component.RewardShopContainer.OnBuyBooster += RequestBoosterBuy;
            Component.RewardShopContainer.OnBuyIngredients += RequestIngredientBuy;
            Component.RewardShopContainer.OnBuySlot += RequestSlotBuy;
            Component.RewardShopContainer.OnContinue += Continue;
            Component.RewardShopContainer.SetVisible(false);
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            var levelCompleteSystem = (LevelCompleteSystem)gameSystemsHandler.GetGameSystem(typeof(LevelCompleteSystem));
            if (!levelCompleteSystem.Component.IsLevelCompleted)
            {
                gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.ChangeLevel);
                return;
            }

            if (!Component.IsRewardStateInitialized)
            {
                EnterRewardState(gameSystemsHandler);
            }

            ProcessBuyRequests(gameSystemsHandler);

            if (Component.IsContinuePressed || Component.RewardShopContainer == null)
            {
                ExitRewardState(gameSystemsHandler);
            }
        }

        public void DisposeMechanic()
        {
            if (Component.RewardShopContainer == null)
            {
                return;
            }

            Component.RewardShopContainer.OnBuyBooster -= RequestBoosterBuy;
            Component.RewardShopContainer.OnBuyIngredients -= RequestIngredientBuy;
            Component.RewardShopContainer.OnBuySlot -= RequestSlotBuy;
            Component.RewardShopContainer.OnContinue -= Continue;
            Component.RewardShopContainer.UnbindButtons();
        }

        private void EnterRewardState(GameSystemsHandler gameSystemsHandler)
        {
            Component.IsRewardStateInitialized = true;
            Component.IsContinuePressed = false;
            Component.IsBoosterPurchasedThisState = false;
            Component.IsIngredientPurchasedThisState = false;
            Component.IsSlotPurchasedThisState = false;
            Component.IsBoosterBuyRequested = false;
            Component.IsIngredientBuyRequested = false;
            Component.IsSlotBuyRequested = false;

            var levelHandler = (LevelHandlerSystem)gameSystemsHandler.GetGameSystem(typeof(LevelHandlerSystem));
            var currencySystem = (PlayerCurrencySystem)gameSystemsHandler.GetGameSystem(typeof(PlayerCurrencySystem));
            var levelAchievement = levelHandler.Component.GetLevelAchievementDescription();

            foreach (var bonus in levelAchievement.BonusElementsForAchieved)
            {
                ElementsStaticInventory.AddElementToInventory(bonus.ElementName);
            }

            var handSystem = (ElementsHandSystem)gameSystemsHandler.GetGameSystem(typeof(ElementsHandSystem));
            handSystem.Component.CurrentHandSlotCapacity += levelAchievement.AdditionalHandSlot;
            handSystem.Component.CurrentSwapCount += levelAchievement.AdditionalSwap;
            handSystem.Mechanic.FixHandCounts();
            SaveHandValues(handSystem.Component);

            if (Component.RewardShopDescription != null)
            {
                currencySystem.Mechanic.AddCurrency(Component.RewardShopDescription.CurrencyRewardPerWin);
            }

            ElementsStaticInventory.SaveCurrentAvailableElements();
            RefreshUI(gameSystemsHandler);
            if (Component.RewardShopContainer != null)
            {
                Component.RewardShopContainer.SetVisible(true);
            }
        }

        private void ExitRewardState(GameSystemsHandler gameSystemsHandler)
        {
            if (Component.RewardShopContainer != null)
            {
                Component.RewardShopContainer.SetVisible(false);
            }

            Component.IsRewardStateInitialized = false;
            gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.ChangeLevel);
        }

        private void BuyBoosterOffer(GameSystemsHandler systemsHandler)
        {
            if (Component.IsBoosterPurchasedThisState || Component.RewardShopDescription?.BoosterOffer == null)
            {
                return;
            }

            var currencySystem = (PlayerCurrencySystem)systemsHandler.GetGameSystem(typeof(PlayerCurrencySystem));
            var boostersSystem = (BoostersSystem)systemsHandler.GetGameSystem(typeof(BoostersSystem));
            var offer = Component.RewardShopDescription.BoosterOffer;

            if (boostersSystem.Mechanic.IsBoosterPurchased(offer.BoosterName))
            {
                Component.IsBoosterPurchasedThisState = true;
                RefreshUI(systemsHandler);
                return;
            }

            if (!currencySystem.Mechanic.TrySpend(offer.Price))
            {
                RefreshUI(systemsHandler);
                return;
            }

            if (!boostersSystem.Mechanic.TryUnlockBoosterPermanent(offer.BoosterName))
            {
                currencySystem.Mechanic.AddCurrency(offer.Price);
                RefreshUI(systemsHandler);
                return;
            }

            Component.IsBoosterPurchasedThisState = true;
            RefreshUI(systemsHandler);
        }

        private void BuyIngredientOffer(GameSystemsHandler systemsHandler)
        {
            if (Component.IsIngredientPurchasedThisState || Component.RewardShopDescription?.IngredientOffer == null)
            {
                return;
            }

            var currencySystem = (PlayerCurrencySystem)systemsHandler.GetGameSystem(typeof(PlayerCurrencySystem));
            var offer = Component.RewardShopDescription.IngredientOffer;

            if (!currencySystem.Mechanic.TrySpend(offer.Price))
            {
                RefreshUI(systemsHandler);
                return;
            }

            foreach (var rewardItem in offer.Items)
            {
                if (rewardItem.Element == null || rewardItem.Count <= 0)
                {
                    continue;
                }

                for (var i = 0; i < rewardItem.Count; i++)
                {
                    ElementsStaticInventory.AddElementToInventory(rewardItem.Element.ElementName);
                }
            }

            ElementsStaticInventory.SaveCurrentAvailableElements();
            Component.IsIngredientPurchasedThisState = true;
            RefreshUI(systemsHandler);
        }

        private void BuySlotOffer(GameSystemsHandler systemsHandler)
        {
            if (Component.IsSlotPurchasedThisState || Component.RewardShopDescription?.HandSlotOffer == null)
            {
                return;
            }

            var currencySystem = (PlayerCurrencySystem)systemsHandler.GetGameSystem(typeof(PlayerCurrencySystem));
            var handSystem = (ElementsHandSystem)systemsHandler.GetGameSystem(typeof(ElementsHandSystem));
            var offer = Component.RewardShopDescription.HandSlotOffer;

            if (!currencySystem.Mechanic.TrySpend(offer.Price))
            {
                RefreshUI(systemsHandler);
                return;
            }

            handSystem.Component.CurrentHandSlotCapacity += offer.SlotIncrement;
            handSystem.Mechanic.FixHandCounts();
            SaveHandValues(handSystem.Component);

            Component.IsSlotPurchasedThisState = true;
            RefreshUI(systemsHandler);
        }

        private void Continue()
        {
            Component.IsContinuePressed = true;
        }

        private void RequestBoosterBuy()
        {
            Component.IsBoosterBuyRequested = true;
        }

        private void RequestIngredientBuy()
        {
            Component.IsIngredientBuyRequested = true;
        }

        private void RequestSlotBuy()
        {
            Component.IsSlotBuyRequested = true;
        }

        private void ProcessBuyRequests(GameSystemsHandler systemsHandler)
        {
            if (Component.IsBoosterBuyRequested)
            {
                Component.IsBoosterBuyRequested = false;
                BuyBoosterOffer(systemsHandler);
            }

            if (Component.IsIngredientBuyRequested)
            {
                Component.IsIngredientBuyRequested = false;
                BuyIngredientOffer(systemsHandler);
            }

            if (Component.IsSlotBuyRequested)
            {
                Component.IsSlotBuyRequested = false;
                BuySlotOffer(systemsHandler);
            }
        }

        private void RefreshUI(GameSystemsHandler gameSystemsHandler)
        {
            if (Component.RewardShopContainer == null || Component.RewardShopDescription == null)
            {
                return;
            }

            var currencySystem = (PlayerCurrencySystem)gameSystemsHandler.GetGameSystem(typeof(PlayerCurrencySystem));
            var boostersSystem = (BoostersSystem)gameSystemsHandler.GetGameSystem(typeof(BoostersSystem));
            var balance = currencySystem.Mechanic.GetBalance();

            if (Component.RewardShopContainer.CurrencyText != null)
            {
                Component.RewardShopContainer.CurrencyText.text = balance.ToString();
            }

            var boosterOffer = Component.RewardShopDescription.BoosterOffer;
            if (boosterOffer != null && Component.RewardShopContainer.BoosterOfferButton != null)
            {
                var purchased = Component.IsBoosterPurchasedThisState || boostersSystem.Mechanic.IsBoosterPurchased(boosterOffer.BoosterName);
                Component.RewardShopContainer.BoosterOfferButton.SetPrice(boosterOffer.Price);
                Component.RewardShopContainer.BoosterOfferButton.SetState(balance >= boosterOffer.Price, purchased);
            }

            var ingredientOffer = Component.RewardShopDescription.IngredientOffer;
            if (ingredientOffer != null && Component.RewardShopContainer.IngredientOfferButton != null)
            {
                Component.RewardShopContainer.IngredientOfferButton.SetPrice(ingredientOffer.Price);
                Component.RewardShopContainer.IngredientOfferButton.SetState(balance >= ingredientOffer.Price, Component.IsIngredientPurchasedThisState);
            }

            var handSlotOffer = Component.RewardShopDescription.HandSlotOffer;
            if (handSlotOffer != null && Component.RewardShopContainer.SlotOfferButton != null)
            {
                Component.RewardShopContainer.SlotOfferButton.SetPrice(handSlotOffer.Price);
                Component.RewardShopContainer.SlotOfferButton.SetState(balance >= handSlotOffer.Price, Component.IsSlotPurchasedThisState);
            }

            if (Component.RewardShopContainer.ContinueButton != null)
            {
                Component.RewardShopContainer.ContinueButton.SetState(true, false);
            }
        }

        private static void SaveHandValues(ElementsHandComponent component)
        {
            PlayerPrefs.SetInt("ElementsHandCapacity", component.CurrentHandSlotCapacity);
            PlayerPrefs.SetInt("ElementsHandSwapCount", component.CurrentSwapCount);
        }
    }
}

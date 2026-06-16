using System;
using System.Collections.Generic;
using CoffeeScripts;
using UnityEngine;

namespace GameSystemsScripts.CoffeeSystemsScripts.RewardElementsSystemScripts
{
    [CreateAssetMenu(menuName = "Coffee/RewardShopDescription", fileName = "RewardShopDescription", order = 0)]
    public class RewardShopDescription : ScriptableObject
    {
        [Header("Progress")]
        public int CurrencyRewardPerWin = 10;

        [Header("Offers")]
        public BoosterOfferDescription BoosterOffer;
        public IngredientOfferDescription IngredientOffer;
        public HandSlotOfferDescription HandSlotOffer;
    }

    [Serializable]
    public class BoosterOfferDescription
    {
        public string OfferId = "booster_offer";
        public string BoosterName;
        public int Price = 20;
    }

    [Serializable]
    public class IngredientOfferDescription
    {
        public string OfferId = "ingredient_offer";
        public int Price = 10;
        public List<IngredientRewardItem> Items = new();
    }

    [Serializable]
    public class HandSlotOfferDescription
    {
        public string OfferId = "slot_offer";
        public int Price = 15;
        public int SlotIncrement = 1;
    }

    [Serializable]
    public class IngredientRewardItem
    {
        public CoffeeElementDescription Element;
        public int Count = 1;
    }
}

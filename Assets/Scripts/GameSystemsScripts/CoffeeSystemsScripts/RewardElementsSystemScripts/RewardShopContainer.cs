using System;
using TMPro;
using UnityEngine;

namespace GameSystemsScripts.CoffeeSystemsScripts.RewardElementsSystemScripts
{
    public class RewardShopContainer : MonoBehaviour
    {
        public GameObject RootObject;
        public TMP_Text CurrencyText;

        public RewardShopOfferButton BoosterOfferButton;
        public RewardShopOfferButton IngredientOfferButton;
        public RewardShopOfferButton SlotOfferButton;
        public RewardShopOfferButton ContinueButton;

        public Action OnBuyBooster;
        public Action OnBuyIngredients;
        public Action OnBuySlot;
        public Action OnContinue;

        public void BindButtons()
        {
            if (BoosterOfferButton != null)
            {
                BoosterOfferButton.OnPressed += HandleBuyBooster;
            }

            if (IngredientOfferButton != null)
            {
                IngredientOfferButton.OnPressed += HandleBuyIngredients;
            }

            if (SlotOfferButton != null)
            {
                SlotOfferButton.OnPressed += HandleBuySlot;
            }

            if (ContinueButton != null)
            {
                ContinueButton.OnPressed += HandleContinue;
            }
        }

        public void UnbindButtons()
        {
            if (BoosterOfferButton != null)
            {
                BoosterOfferButton.OnPressed -= HandleBuyBooster;
            }

            if (IngredientOfferButton != null)
            {
                IngredientOfferButton.OnPressed -= HandleBuyIngredients;
            }

            if (SlotOfferButton != null)
            {
                SlotOfferButton.OnPressed -= HandleBuySlot;
            }

            if (ContinueButton != null)
            {
                ContinueButton.OnPressed -= HandleContinue;
            }
        }

        public void SetVisible(bool value)
        {
            if (RootObject != null)
            {
                RootObject.SetActive(value);
            }
            else
            {
                gameObject.SetActive(value);
            }
        }

        private void HandleBuyBooster()
        {
            OnBuyBooster?.Invoke();
        }

        private void HandleBuyIngredients()
        {
            OnBuyIngredients?.Invoke();
        }

        private void HandleBuySlot()
        {
            OnBuySlot?.Invoke();
        }

        private void HandleContinue()
        {
            OnContinue?.Invoke();
        }
    }
}

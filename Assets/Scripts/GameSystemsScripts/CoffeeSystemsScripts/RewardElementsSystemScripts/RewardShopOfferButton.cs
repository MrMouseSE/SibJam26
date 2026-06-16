using System;
using TMPro;
using UnityEngine;

namespace GameSystemsScripts.CoffeeSystemsScripts.RewardElementsSystemScripts
{
    public class RewardShopOfferButton : MonoBehaviour
    {
        public TMP_Text PriceText;
        public TMP_Text StateText;
        public Collider ButtonCollider;
        public string PurchasedText = "Purchased";
        public string LockedText = "Locked";
        public string AvailableText = "Buy";

        public Action OnPressed;

        public void SetPrice(int price)
        {
            if (PriceText != null)
            {
                PriceText.text = price.ToString();
            }
        }

        public void SetState(bool canBuy, bool purchased)
        {
            if (ButtonCollider != null)
            {
                ButtonCollider.enabled = canBuy && !purchased;
            }

            if (StateText == null)
            {
                return;
            }

            if (purchased)
            {
                StateText.text = PurchasedText;
                return;
            }

            StateText.text = canBuy ? AvailableText : LockedText;
        }

        public void OnMouseUp()
        {
            OnPressed?.Invoke();
        }
    }
}

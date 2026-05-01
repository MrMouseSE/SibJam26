using System;
using TweenScripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.CompleteSelectionSystemScripts
{
    public class ElementDrawStateButtonContainer : MonoBehaviour , IPointerClickHandler
    {
        public GameObject ButtonGameObject;
        public Transform ButtonTrasform;
        
        public TweenGroupAnimation ButtonAnimations;
        
        public Action OnButtonPressed;

        public void SetActive(bool active)
        {
            //Todo: select start animation
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnButtonPressed?.Invoke();
        }
    }
}
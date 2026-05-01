using System;
using TweenScripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.CompleteSelectionSystemScripts
{
    public class CompleteSelectionButtonContainer : MonoBehaviour , IPointerClickHandler
    {
        public GameObject ButtonGameObject;
        public Transform ButtonTrasform;
        
        public TweenGroupAnimation ButtonAnimations;
        
        public Action OnButtonPressed;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnButtonPressed?.Invoke();
        }
    }
}
using System.Collections.Generic;
using TweenScripts;
using UnityEngine;

namespace CoffeeScripts.ElementsInventoryScripts
{
    public class ElementsDrawHandlerContainer : MonoBehaviour
    {
        public GameObject HandlerGameObject;
        public Transform HandlerTrasform;
        
        public List<ElementContainer> ElementContainers;

        public TweenAnimation AppearAnimation;
    }
}
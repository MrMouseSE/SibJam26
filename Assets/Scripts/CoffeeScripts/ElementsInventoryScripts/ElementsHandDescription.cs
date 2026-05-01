using UnityEngine;

namespace CoffeeScripts.ElementsInventoryScripts
{
    [CreateAssetMenu(menuName = "Create ElementsHandDescription", fileName = "ElementsHandDescription", order = 0)]
    public class ElementsHandDescription : ScriptableObject
    {
        public Vector2Int MinMaxElementsHandCapacity;
        public Vector2Int MinMaxElementSwapCount;
    }
}
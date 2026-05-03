using System.Collections.Generic;
using UnityEngine;

namespace CoffeeScripts.ElementsInventoryScripts
{
    public static class ElementsStaticInventory
    {
        public static readonly Dictionary<string, int> CurrentAvailableElements = new();

        public static void AddElementToInventory(string element)
        {
            CurrentAvailableElements[element]++;
        }

        public static void RemoveElementInInventory(string element)
        {
            CurrentAvailableElements[element]--;
        }

        public static void SaveCurrentAvailableElements()
        {
            foreach (KeyValuePair<string, int> pair in CurrentAvailableElements)
            {
                PlayerPrefs.SetInt(pair.Key, pair.Value);
            }
        }

        public static void LoadCurrentAvailableElements(CoffeeDescription description)
        {
            foreach (var element in description.Elements)
            {
                CurrentAvailableElements.Add(element.ElementName, PlayerPrefs.GetInt(element.ElementName, 0));
            }
        }
    }
}
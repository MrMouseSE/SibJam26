using System;
using System.Collections.Generic;
using CoffeeScripts;
using UnityEngine;

namespace GameSystemsScripts.CoffeeSystemsScripts.BoostersSystemScripts
{
    [CreateAssetMenu(menuName = "Coffee/BoostersDescription", fileName = "BoostersDescription", order = 0)]
    public class BoostersDescription : ScriptableObject
    {
        public List<BoosterDescription> Boosters = new();
    }

    [Serializable]
    public class BoosterDescription
    {
        public string BoosterName;
        public bool IsEnabled = true;
        public float Multiplier = 1f;

        [Header("Conditions")]
        public List<string> RequiredElementNames = new();
        public List<ElementType> RequiredElementTypes = new();
    }
}

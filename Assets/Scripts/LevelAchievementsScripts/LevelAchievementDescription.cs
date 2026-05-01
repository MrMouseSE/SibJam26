using System.Collections.Generic;
using CoffeeScripts;
using UnityEngine;

namespace LevelAchievementsScripts
{
    [CreateAssetMenu(menuName = "Create LevelAchievementDescription", fileName = "Levels/LevelAchievementDescription", order = 0)]
    public class LevelAchievementDescription :ScriptableObject
    {
        public int Level;
        public float LevelValue;

        public List<CoffeeElementDescription> BonusElementsForAchieved;
    }
}
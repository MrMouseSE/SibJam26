using System.Collections.Generic;
using CoffeeScripts;
using UnityEngine;

namespace LevelAchievementsScripts
{
    [CreateAssetMenu(menuName = "Levels/LevelAchievementDescription", fileName = "LevelAchievementDescription", order = 0)]
    public class LevelAchievementDescription :ScriptableObject
    {
        public int Level;
        public float LevelScoreToAchieve;

        public List<CoffeeElementDescription> BonusElementsForAchieved;

        public int AdditionalHandSlot;
        public int AdditionalSwap;
    }
}
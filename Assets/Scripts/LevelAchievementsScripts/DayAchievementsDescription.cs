using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LevelAchievementsScripts
{
    [CreateAssetMenu(menuName = "Create DayAchievementsDescription", fileName = "Levels/DayAchievementsDescription", order = 0)]
    public class DayAchievementsDescription : ScriptableObject
    {
        public List<LevelAchievementDescription> LevelsAchievements;

        private void OnValidate()
        {
            LevelsAchievements = LevelsAchievements.OrderBy(x=>x.Level).ToList();
        }
    }
}
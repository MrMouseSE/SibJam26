using System.Collections.Generic;
using UnityEngine;

namespace LevelAchievementsScripts
{
    [CreateAssetMenu(menuName = "Levels/DaysAchievementsDescription", fileName = "DaysAchievementsDescription", order = 0)]
    public class DaysAchievementsDescription : ScriptableObject
    {
        public List<DayAchievementsDescription> DaysAchievementsDescriptions;
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace LevelAchievementsScripts
{
    [CreateAssetMenu(menuName = "Create DaysAchievementsDescription", fileName = "Level/DaysAchievementsDescription", order = 0)]
    public class DaysAchievementsDescription : ScriptableObject
    {
        public List<DayAchievementsDescription> DaysAchievementsDescriptions;
    }
}
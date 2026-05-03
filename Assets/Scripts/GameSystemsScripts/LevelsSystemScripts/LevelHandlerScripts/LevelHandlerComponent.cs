using LevelAchievementsScripts;
using SoundsComponentsScripts;
using UnityEditor.Experimental.GraphView;

namespace GameSystemsScripts.LevelsSystemScripts.LevelHandlerScripts
{
    public class LevelHandlerComponent
    {
        public SoundContainer AudioContainer;
        public DaysAchievementsDescription DaysDescription;
        public int Level;
        public int Day;

        public LevelHandlerComponent(DaysAchievementsDescription description)
        {
            DaysDescription = description;
        }

        public LevelAchievementDescription GetLevelAchievementDescription()
        {
            return DaysDescription.DaysAchievementsDescriptions[Day].LevelsAchievements[Level];
        }
    }
}
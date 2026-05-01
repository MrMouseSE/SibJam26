using LevelAchievementsScripts;

namespace GameSystemsScripts.LevelsSystemScripts.LevelCompleteScripts
{
    public class LevelCompleteComponent
    {
        public bool IsLevelCompleted;
        public DaysAchievementsDescription DaysDescription;

        public LevelCompleteComponent(DaysAchievementsDescription dayDescription)
        {
            DaysDescription = dayDescription;
        }
    }
}
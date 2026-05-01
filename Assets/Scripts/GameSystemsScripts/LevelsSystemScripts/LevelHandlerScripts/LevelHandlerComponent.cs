using LevelAchievementsScripts;

namespace GameSystemsScripts.LevelsSystemScripts.LevelHandlerScripts
{
    public class LevelHandlerComponent
    {
        public DaysAchievementsDescription DaysDescription;
        public int Level;
        public int Day;

        public LevelHandlerComponent(DaysAchievementsDescription description)
        {
            DaysDescription = description;
        }
    }
}
using GameScripts;
using GameSystemsScripts.LevelsSystemScripts.GameCompleteSystemScripts;
using GameSystemsScripts.LevelsSystemScripts.LevelCompleteScripts;
using ScenesOperatingScripts;
using SoundsComponentsScripts;

namespace GameSystemsScripts.LevelsSystemScripts.LevelHandlerScripts
{
    public class LevelHandlerMechanic : IGameMechanic
    {
        public LevelHandlerComponent Component;

        public LevelHandlerMechanic(LevelHandlerComponent component)
        {
            Component = component;
        }

        public void SetSoundContainer(SoundContainer soundContainer)
        {
            Component.AudioContainer = soundContainer;
            Component.AudioContainer.AppearClips = new SoundPair[1] { Component.DaysDescription.WinClip };
            Component.AudioContainer.DeathClips = new SoundPair[1] { Component.DaysDescription.LoseClip };
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            var levelCompleteSystem = (LevelCompleteSystem)gameSystemsHandler.GetGameSystem(typeof(LevelCompleteSystem)); 
            if (levelCompleteSystem.Component.IsLevelCompleted)
            {
                if (Component.Level == Component.DaysDescription.DaysAchievementsDescriptions[Component.Day].LevelsAchievements.Count)
                {
                    Component.Level = 0;
                    if (Component.Day < Component.DaysDescription.DaysAchievementsDescriptions.Count)
                    {
                        Component.Day++;
                    }
                    else
                    {
                        var completeSystem = (GameCompleteSystem)gameSystemsHandler.GetGameSystem(typeof(GameCompleteSystem));
                        completeSystem.Component.IsGameComplete = true;
                    }
                }
                else
                {
                    Component.AudioContainer.Play(SoundType.AppearSound);
                    Component.Level++;
                }
            }
            else
            {
                Component.AudioContainer.Play(SoundType.DeathSound);
                Component.Level = 0;
                Component.Day = 0;
            }
            
            gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.UpdateLevelView);
        }

        public void DisposeMechanic()
        {
        }
    }
}
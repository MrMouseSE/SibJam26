using GameSystemsScripts.GameCharactersSystems.AdventureResultSystem;
using ScenesOperatingScripts;

namespace GameSystemsScripts.GlobalMapSystems.GlobalMapCharactersGroupSystem
{
    public class GlobalMapCharactersGroupMechanic : IGameMechanic
    {
        public GlobalMapCharactersGroupComponent Component;
        public GlobalMapCharactersGroupMechanic(GlobalMapCharactersGroupComponent component)
        {
            Component = component;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (Component.CharactersGroups.Count == 0) return;
            foreach (var charactersGroup in Component.CharactersGroups)
            {
                if (!charactersGroup.IsGroupInAdventureProcess) continue;
                charactersGroup.AdventureProgressValue += 
                    charactersGroup.CurrentPointOfInterest.Container.TravelSpeed * deltaTime * charactersGroup.SpeedBackwardMultiplier;
                if (charactersGroup.AdventureProgressValue > 1f)
                {
                    charactersGroup.AdventureForward = false;
                    ((AdventureResultSystem)gameSystemsHandler.
                        GetGameSystem(typeof(AdventureResultSystem))).Component.CharactersGroups.Add(charactersGroup);
                }

                if (charactersGroup.AdventureProgressValue < 0f)
                {
                    charactersGroup.IsGroupInAdventureProcess = false;
                    charactersGroup.AdventureProgressValue = 0f;
                    charactersGroup.PreviousPointOfInterest = charactersGroup.CurrentPointOfInterest;
                    charactersGroup.CurrentPointOfInterest = null;
                    continue;
                }
                
                charactersGroup.GroupTransform.position =
                    charactersGroup.CurrentPointOfInterest.Container.TravelPath.EvaluatePosition(charactersGroup
                        .AdventureProgressValue);
            }
        }

        public void DisposeMechanic()
        {
        }
    }
}
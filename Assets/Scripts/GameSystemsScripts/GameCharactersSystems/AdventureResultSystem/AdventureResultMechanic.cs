using System.Collections.Generic;
using System.Linq;
using GameSystemsScripts.GlobalMapSystems.GlobalMapCharactersGroupSystem;
using ScenesOperatingScripts;

namespace GameSystemsScripts.GameCharactersSystems.AdventureResultSystem
{
    public class AdventureResultMechanic : IGameMechanic
    {
        public AdventureResultComponent Component;

        public AdventureResultMechanic(AdventureResultComponent component)
        {
            Component = component;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            List<CharactersGroupContainer> groupsCalculated = new List<CharactersGroupContainer>();
            foreach (var charactersGroup in Component.CharactersGroups)
            {
                //Todo: calculate adventure result
                
                charactersGroup.SpeedBackwardMultiplier = -1f;
                charactersGroup.AdventureProgressValue = 1f;
            }
            Component.CharactersGroups = Component.CharactersGroups.Except(groupsCalculated).ToList();
        }

        public void DisposeMechanic()
        {
        }
    }
}
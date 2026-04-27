using GameSystemsScripts.GameCharactersSystems.CharactersGroupSystem;
using GameSystemsScripts.GlobalMapSystems.PointsOfInterest;
using UnityEngine;

namespace GameSystemsScripts.GlobalMapSystems.GlobalMapCharactersGroupSystem
{
    public class CharactersGroupsContainer : MonoBehaviour
    {
        public Transform GroupTransform;
        public GameObject GroupObject;
        
        public CharacterGroupComponent CharacterGroup;
        public PointOfInterestComponent PointOfInterest;
        
        public bool IsGroupInAdventureProcess;
        public float AdventureProgressValue;
        public bool AdventureForward;
    }
}
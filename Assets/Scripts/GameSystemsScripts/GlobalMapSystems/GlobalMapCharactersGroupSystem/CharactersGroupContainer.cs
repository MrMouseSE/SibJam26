using GameSystemsScripts.GameCharactersSystems.CharactersGroupSystem;
using GameSystemsScripts.GlobalMapSystems.PointsOfInterest;
using UnityEngine;

namespace GameSystemsScripts.GlobalMapSystems.GlobalMapCharactersGroupSystem
{
    public class CharactersGroupContainer : MonoBehaviour
    {
        public Transform GroupTransform;
        public GameObject GroupObject;
        
        public CharacterGroupComponent CharacterGroup;
        public PointOfInterestComponent CurrentPointOfInterest;
        public PointOfInterestComponent PreviousPointOfInterest;
        
        public bool IsGroupInAdventureProcess;
        public float AdventureProgressValue;
        public bool AdventureForward;
        public float SpeedBackwardMultiplier = 1f;
    }
}
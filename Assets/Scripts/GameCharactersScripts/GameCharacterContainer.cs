using TMPro;
using UnityEngine;

namespace GameCharactersScripts
{
    public class GameCharacterContainer : MonoBehaviour
    {
        public Transform CharacterTransform;
        public Sprite CharacterSprite;
        
        public TMP_Text CharacterName;
        
        public CharacterItemSlot[] CharacterItemSlots;
    }
}
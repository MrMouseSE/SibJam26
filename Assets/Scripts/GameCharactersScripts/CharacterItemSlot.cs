using System;
using UnityEngine;

namespace GameCharactersScripts
{
    [Serializable]
    public class CharacterItemSlot
    {
        public bool IsSlotOccupied;
        public ItemType[] AvailableItemTypes;
        public Transform SlotTransform;
    }
}
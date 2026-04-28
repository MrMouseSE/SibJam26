using GameCharactersScripts;

namespace GameSystemsScripts.GameCharactersSystems.CharacterSystem
{
    public class CharacterComponent
    {
        public GameCharacterContainer Container;
        
        public CharacterHistoryDescription CharacterHistoryDescription;
        
        public float MaxHits;
        public float MaxMorale;
        public float MaxDamage;
        public float MaxSlotAvailable;
        
        public float CurrentHits;
        public float CurrentMorale;
        public float CurrentDamage;
        public float CurrentSlotAvailable;
    }
}
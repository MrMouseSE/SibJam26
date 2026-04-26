using System;

namespace GameCharactersScripts
{
    [Serializable]
    public class CharacterGenerationPercentHolder
    {
        public string CharactersDifficulty;
        public float GetPercentage;
        public CharacterGenerationDescription[] CharactersDescriptions;
    }
}
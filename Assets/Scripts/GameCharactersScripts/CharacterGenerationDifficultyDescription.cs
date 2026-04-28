using System;

namespace GameCharactersScripts
{
    [Serializable]
    public class CharacterGenerationDifficultyDescription
    {
        public string CharactersDifficulty;
        public float GetPercentage;
        public CharacterGenerationDescription[] CharactersDescriptions;
    }
}
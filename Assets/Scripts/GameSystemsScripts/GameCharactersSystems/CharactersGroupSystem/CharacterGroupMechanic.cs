using ScenesOperatingScripts;

namespace GameSystemsScripts.GameCharactersSystems.CharactersGroupSystem
{
    public class CharacterGroupMechanic : IGameMechanic
    {
        public CharacterGroupComponent Component;

        public CharacterGroupMechanic(CharacterGroupComponent component)
        {
            Component = component;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
        }

        public void DisposeMechanic()
        {
        }
    }
}
using ScenesOperatingScripts;

namespace GameSystemsScripts.GameCharactersSystems.CharacterSystem
{
    public class CharacterMechanic : IGameMechanic
    {
        public CharacterComponent Component;

        public CharacterMechanic(CharacterComponent component)
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
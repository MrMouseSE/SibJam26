using ScenesOperatingScripts;

namespace GameSystemsScripts.GameCharactersSystems.CharacterSystem
{
    public class CharacterSystem : IGameSystem
    {
        public CharacterComponent Component;
        public CharacterMechanic Mechanic;

        public CharacterSystem(CharacterComponent component)
        {
            Component = component;
            Mechanic = new CharacterMechanic(component);
        }

        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
        }

        public void DisposeSystem()
        {
        }
    }
}
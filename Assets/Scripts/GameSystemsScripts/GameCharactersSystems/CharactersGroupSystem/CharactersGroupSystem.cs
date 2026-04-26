using ScenesOperatingScripts;

namespace GameSystemsScripts.GameCharactersSystems.CharactersGroupSystem
{
    public class CharactersGroupSystem : IGameSystem
    {
        public CharacterGroupComponent Component;
        public CharacterGroupMechanic Mechanic;

        public CharactersGroupSystem(CharacterGroupComponent component)
        {
            Component = component;
            Mechanic = new CharacterGroupMechanic(component);
        }

        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            throw new System.NotImplementedException();
        }

        public void DisposeSystem()
        {
            throw new System.NotImplementedException();
        }
    }
}
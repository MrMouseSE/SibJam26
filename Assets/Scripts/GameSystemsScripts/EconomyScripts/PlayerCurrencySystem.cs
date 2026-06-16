using ScenesOperatingScripts;

namespace GameSystemsScripts.EconomyScripts
{
    public class PlayerCurrencySystem : IGameSystem
    {
        public PlayerCurrencyComponent Component;
        public PlayerCurrencyMechanic Mechanic;

        public PlayerCurrencySystem()
        {
            Component = new PlayerCurrencyComponent();
            Mechanic = new PlayerCurrencyMechanic(Component);
        }

        public void Initialize(GameSystemsHandler gameSystemsHandler)
        {
            Mechanic.Load();
        }

        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
        }

        public void DisposeSystem()
        {
        }
    }
}

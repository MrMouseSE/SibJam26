using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.BoilingSystemScripts
{
    public class BoilingMechanic : IGameMechanic
    {
        public BoilingComponent Component;

        public BoilingMechanic(BoilingComponent component)
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
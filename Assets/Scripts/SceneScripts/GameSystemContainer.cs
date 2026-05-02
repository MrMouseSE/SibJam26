using CoffeeScripts.ElementsInventoryScripts;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsDrawScripts;
using ScenesOperatingScripts;

namespace SceneScripts
{
    public class GameSystemContainer : SceneSystemsContainer
    {
        public ElementsDrawHandlerContainer ElementsDrawHandlerContainer;

        public override void InitializeSceneSystems(GameSystemsHandler systemsHandler)
        {
            var drawSystem = (ElementsDrawSystem)systemsHandler.GetGameSystem(typeof(ElementsDrawSystem));
            drawSystem.Component.Container = ElementsDrawHandlerContainer;

        }
    }
}

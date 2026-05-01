using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsHandSystemScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsRedrawScripts
{
    public class ElementsRedrawMechanic : IGameMechanic
    {
        public ElementsRedrawComponent Component;

        public ElementsRedrawMechanic(ElementsRedrawComponent component)
        {
            Component = component;
            Component.RedrawButtonContainer.OnButtonPressed += RedrawButtonPushed;
        }

        private void RedrawButtonPushed()
        {
            Component.IsRedrawButtonPressed = true;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (!Component.IsRedrawButtonPressed) return;
            
            Component.IsRedrawButtonPressed = false;
            var handSystem = (ElementsHandSystem)gameSystemsHandler.GetGameSystem(typeof(ElementsHandSystem));
            handSystem.Component.UnusedSwaps --;
            if (handSystem.Component.UnusedSwaps == 0)
            {
                Component.RedrawButtonContainer.SetActive(false);
            }
            //TODO: redraw selected elements
        }

        public void DisposeMechanic()
        {
        }
    }
}
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.CompleteSelectionSystemScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsRedrawScripts
{
    public class ElementsRedrawComponent
    {
        public bool IsRedrawButtonPressed;
        public ElementDrawStateButtonContainer RedrawButtonContainer;

        public ElementsRedrawComponent(ElementDrawStateButtonContainer redrawButtonContainer)
        {
            RedrawButtonContainer = redrawButtonContainer;
        }
    }
}
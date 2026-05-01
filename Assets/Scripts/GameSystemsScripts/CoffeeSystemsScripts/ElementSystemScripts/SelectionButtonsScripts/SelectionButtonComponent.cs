using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.CompleteSelectionSystemScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.SelectionButtonsScripts
{
    public class SelectionButtonComponent
    {
        public bool IsSelectionStateStartedThisFrame;
        
        public ElementDrawStateButtonContainer CompleteButton;
        public ElementDrawStateButtonContainer RedrawButton;

        public SelectionButtonComponent(ElementDrawStateButtonContainer completeButton, ElementDrawStateButtonContainer redrawButton)
        {
            CompleteButton = completeButton;
            RedrawButton = redrawButton;
        }
    }
}
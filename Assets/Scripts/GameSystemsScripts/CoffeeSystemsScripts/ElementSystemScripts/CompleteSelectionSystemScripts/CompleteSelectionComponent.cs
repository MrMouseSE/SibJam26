namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.CompleteSelectionSystemScripts
{
    public class CompleteSelectionComponent
    {
        public bool IsButtonPressedThisFrame;
        
        public ElementDrawStateButtonContainer Container;

        public CompleteSelectionComponent(ElementDrawStateButtonContainer container)
        {
            Container = container;
        }
    }
}
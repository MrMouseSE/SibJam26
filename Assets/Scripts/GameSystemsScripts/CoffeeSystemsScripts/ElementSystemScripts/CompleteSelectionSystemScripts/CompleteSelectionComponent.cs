namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.CompleteSelectionSystemScripts
{
    public class CompleteSelectionComponent
    {
        public bool IsButtonPressedThisFrame;
        
        public CompleteSelectionButtonContainer Container;

        public CompleteSelectionComponent(CompleteSelectionButtonContainer container)
        {
            Container = container;
        }
    }
}
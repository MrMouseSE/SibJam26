using CoffeeScripts.ElementsInventoryScripts;
using GameSystemsScripts.CoffeeSystemsScripts.BoilingSystemScripts.BoilingProcessScripts;
using GameSystemsScripts.CoffeeSystemsScripts.BoilingSystemScripts.CompleteBoilingScripts;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.CompleteSelectionSystemScripts;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsDrawScripts;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsRedrawScripts;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.SelectionButtonsScripts;
using GameSystemsScripts.ScoreViewScripts.InterfaceScoreScripts;

namespace ScenesOperatingScripts
{
    public class GameSystemContainer : SceneSystemsContainer
    {
        public BoilCoffeeContainer BoilCoffeeContainer;
        
        public GameButtonContainer RedrawButtonContainer;
        public GameButtonContainer CompleteButtonContainer;
        public BoilButtonContainer BoilCompleteButtonContainer;
        public ElementsDrawHandlerContainer ElementsDrawHandlerContainer;
        public InterfaceScoreContainer InterfaceScoreContainer;

        public override void InitializeSceneSystems(GameSystemsHandler systemsHandler)
        {
            var drawSystem = (ElementsDrawSystem)systemsHandler.GetGameSystem(typeof(ElementsDrawSystem));
            ElementsDrawHandlerContainer.AppearAnimation.SetForceState(true);
            drawSystem.Component.Container = ElementsDrawHandlerContainer;
            
            var selectionButtonsSystem = (SelectionButtonsSystem)systemsHandler.GetGameSystem(typeof(SelectionButtonsSystem));
            selectionButtonsSystem.Mechanic.SetButtonsContainers(CompleteButtonContainer, RedrawButtonContainer, systemsHandler);
            
            var elementsRedrawSystem = (ElementsRedrawSystem)systemsHandler.GetGameSystem(typeof(ElementsRedrawSystem));
            elementsRedrawSystem.Mechanic.SetButtonContainer(RedrawButtonContainer, systemsHandler);
            
            var completeSelectionSystem = (CompleteSelectionSystem)systemsHandler.GetGameSystem(typeof(CompleteSelectionSystem));
            completeSelectionSystem.Mechanic.SetButtonContainer(CompleteButtonContainer);
            
            var boilingProcessSystem = (BoilingProcessSystem)systemsHandler.GetGameSystem(typeof(BoilingProcessSystem));
            boilingProcessSystem.Mechanic.SetButtonContainer(BoilCoffeeContainer);
            
            var completeBoilingSystem = (CompleteBoilingSystem)systemsHandler.GetGameSystem(typeof(CompleteBoilingSystem));
            completeBoilingSystem.Mechanic.SetButtonContainer(BoilCompleteButtonContainer);
            
            var interfaceSystem = (InterfaceScoreSystem)systemsHandler.GetGameSystem(typeof(InterfaceScoreSystem));
            interfaceSystem.Mechanic.SetContainer(InterfaceScoreContainer);
        }
    }
}

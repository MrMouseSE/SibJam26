using CoffeeScripts.ElementsInventoryScripts;
using GameSystemsScripts.CoffeeSystemsScripts.BoilingSystemScripts.BoilingProcessScripts;
using GameSystemsScripts.CoffeeSystemsScripts.BoilingSystemScripts.CompleteBoilingScripts;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.CompleteSelectionSystemScripts;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsDrawScripts;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsRedrawScripts;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.SelectionButtonsScripts;
using GameSystemsScripts.CoffeeSystemsScripts.RewardElementsSystemScripts;
using GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.ShowCoffeScripts;
using GameSystemsScripts.LevelsSystemScripts.LevelHandlerScripts;
using GameSystemsScripts.ScoreViewScripts.GameTimerScripts;
using GameSystemsScripts.ScoreViewScripts.InterfaceScoreScripts;
using GameSystemsScripts.ScoreViewScripts.LevelViewScripts;
using SoundsComponentsScripts;

namespace ScenesOperatingScripts
{
    public class GameSystemContainer : SceneSystemsContainer
    {
        public ShowCoffeeContainer ShowCoffeeContainer;
        
        public SoundContainer WinLoseSoundsContainer;
        
        public LevelViewContainer DayLevelViewContainer;
        
        public BoilCoffeeContainer BoilCoffeeContainer;
        
        public GameButtonContainer RedrawButtonContainer;
        public GameButtonContainer CompleteButtonContainer;
        public BoilButtonContainer BoilCompleteButtonContainer;
        public ElementsDrawHandlerContainer ElementsDrawHandlerContainer;
        public InterfaceScoreContainer InterfaceScoreContainer;
        public GameTimerContainer GameTimerContainer;
        public RewardShopContainer RewardShopContainer;

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
            interfaceSystem.Mechanic.SetContainer(InterfaceScoreContainer, systemsHandler);

            var timerSystem = (GameTimerSystem)systemsHandler.GetGameSystem(typeof(GameTimerSystem));
            timerSystem.Mechanic.SetContainer(GameTimerContainer);
            
            var levelSystem = (LevelViewSystem)systemsHandler.GetGameSystem(typeof(LevelViewSystem));
            levelSystem.Mechanic.SetViewContainer(DayLevelViewContainer);
            
            var levelHandler = (LevelHandlerSystem)systemsHandler.GetGameSystem(typeof(LevelHandlerSystem));
            levelHandler.Mechanic.SetSoundContainer(WinLoseSoundsContainer);
            
            var showCoffee = (ShowCoffeeSystem)systemsHandler.GetGameSystem(typeof(ShowCoffeeSystem));
            showCoffee.Mechanic.SetContainer(ShowCoffeeContainer, systemsHandler);

            var rewardSystem = (RewardElementsSystem)systemsHandler.GetGameSystem(typeof(RewardElementsSystem));
            rewardSystem.Mechanic.SetContainer(RewardShopContainer);
        }
    }
}

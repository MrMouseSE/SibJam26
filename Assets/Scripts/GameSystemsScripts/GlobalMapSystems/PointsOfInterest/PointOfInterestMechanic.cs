using MainMenuScripts.StartGameSystemScripts;

namespace GlobalMapScripts.GlobalMapPointsOfInterest
{
    public class PointOfInterestMechanic : IGameMechanic
    {
        public PointOfInterestComponent Component;

        public PointOfInterestMechanic(PointOfInterestComponent component)
        {
            Component = component;
        }

        public void DisposeMechanic()
        {
            throw new System.NotImplementedException();
        }
    }
}
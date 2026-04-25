using GameSystemsScripts;
using ScenesOperatingScripts;

namespace GlobalMapScripts.GlobalMapPointsOfInterest
{
    public class PointOfInterestMechanic : IGameMechanic
    {
        public PointOfInterestComponent Component;

        public PointOfInterestMechanic(PointOfInterestComponent component)
        {
            Component = component;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            throw new System.NotImplementedException();
        }

        public void DisposeMechanic()
        {
            throw new System.NotImplementedException();
        }
    }
}
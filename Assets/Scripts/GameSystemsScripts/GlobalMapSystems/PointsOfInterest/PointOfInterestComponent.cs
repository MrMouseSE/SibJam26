using GlobalMapScripts;

namespace GameSystemsScripts.GlobalMapSystems.PointsOfInterest
{
    public class PointOfInterestComponent
    {
        public PointOfInterestContainer Container;

        public bool IsPointInProcess;

        public PointOfInterestComponent(PointOfInterestContainer container)
        {
            Container = container;
        }
    }
}
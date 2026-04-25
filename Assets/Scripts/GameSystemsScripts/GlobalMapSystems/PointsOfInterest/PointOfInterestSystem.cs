using ScenesOperatingScripts;

namespace GlobalMapScripts.GlobalMapPointsOfInterest
{
    public class PointOfInterestSystem : IGameSystem
    {
        public PointOfInterestMechanic Mechanic;
        public PointOfInterestComponent Component;

        public PointOfInterestSystem(PointOfInterestContainer container)
        {
            Component = new PointOfInterestComponent(container);
            Mechanic = new PointOfInterestMechanic(Component);
        }
    
        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            throw new System.NotImplementedException();
        }

        public void DisposeSystem()
        {
            Mechanic.DisposeMechanic();
        }
    }
}
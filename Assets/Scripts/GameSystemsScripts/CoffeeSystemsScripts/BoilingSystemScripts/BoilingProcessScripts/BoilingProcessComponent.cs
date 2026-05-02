using AnimationDescriptionsScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.BoilingSystemScripts.BoilingProcessScripts
{
    public class BoilingProcessComponent
    {
        public float BoilingTime;
        public float BoilValue;
        public float BoilMultiplier;
        
        public BoilDescription BoilDescription;

        public BoilCoffeeContainer Container;

        public BoilingProcessComponent(BoilDescription boilDescription)
        {
            BoilDescription = boilDescription;
        }
        
    }
}
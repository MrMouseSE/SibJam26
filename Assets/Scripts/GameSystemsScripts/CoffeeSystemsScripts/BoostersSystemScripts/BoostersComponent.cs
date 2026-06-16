using System.Collections.Generic;

namespace GameSystemsScripts.CoffeeSystemsScripts.BoostersSystemScripts
{
    public class BoostersComponent
    {
        public BoostersDescription BoostersDescription;
        public float CurrentMultiplier = 1f;
        public List<BoosterItem> AllBoosters = new();
        public List<BoosterItem> ActiveBoosters = new();

        public BoostersComponent(BoostersDescription boostersDescription)
        {
            BoostersDescription = boostersDescription;
        }
    }
}

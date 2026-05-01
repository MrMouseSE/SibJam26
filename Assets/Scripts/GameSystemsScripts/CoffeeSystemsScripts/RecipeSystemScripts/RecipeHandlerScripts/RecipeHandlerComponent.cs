using CoffeeScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.RecipeHandlerScripts
{
    public class RecipeHandlerComponent
    {
        public CoffeeDescription CurrentCoffeeDescription;

        public RecipeHandlerComponent(CoffeeDescription coffeeDescription)
        {
            CurrentCoffeeDescription = coffeeDescription;
        }
    }
}
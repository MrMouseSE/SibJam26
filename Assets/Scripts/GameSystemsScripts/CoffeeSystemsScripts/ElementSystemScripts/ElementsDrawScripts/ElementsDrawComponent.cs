using System.Collections.Generic;
using CoffeeScripts;
using CoffeeScripts.ElementsInventoryScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsDrawScripts
{
    public class ElementsDrawComponent
    {
        public ElementsDrawHandlerContainer Container;
        public readonly List<ElementContainer> DrawedElements = new List<ElementContainer>();
    }
}
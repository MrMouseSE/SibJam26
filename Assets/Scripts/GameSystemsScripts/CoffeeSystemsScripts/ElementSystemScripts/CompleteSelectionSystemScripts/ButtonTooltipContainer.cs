using System.Threading;
using TMPro;
using TweenScripts;
using UnityEngine;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.CompleteSelectionSystemScripts
{
    public class ButtonTooltipContainer : MonoBehaviour
    {
        public TMP_Text TooltipText;
        public TweenAnimation AppearAnimation;
        
        public CancellationTokenSource CancelToken;
    }
}
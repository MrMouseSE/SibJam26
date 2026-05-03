using System.Threading;
using SoundsComponentsScripts;
using TMPro;
using TweenScripts;
using UnityEngine;

namespace GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.ShowCoffeScripts
{
    public class ShowCoffeeContainer : MonoBehaviour
    {
        public GameObject CoffeShowPrefab;
        public Transform CoffeShowTransform;
        public AudioContainer CoffeShowAudioContainer;

        public TweenAnimation CoffeShowAnimation;
        public SpriteRenderer CoffeShowRenderer;
        
        public TMP_Text ElementsVigodText;
        public TMP_Text ElementsVigodMultText;
        public TMP_Text ElementsTasteText;
        public TMP_Text ElementTasteMultText;
        public TMP_Text CoffeNameText;
        public TMP_Text CoffeValueText;
        public TMP_Text CoffeMultText;
        public TMP_Text BoilValueText;
        public TMP_Text FullScoreText;

        [Space]
        public CountTweenAnimation Vigor;
        public CountTweenAnimation VigorMult;
        public CountTweenAnimation Taste;
        public CountTweenAnimation TasteMult;
        public CountTweenAnimation Coffe;
        public CountTweenAnimation CoffeMult;
        public CountTweenAnimation Boil;
        public CountTweenAnimation Full;
        

        public CancellationTokenSource CancelToken = new CancellationTokenSource();

        
    }
}
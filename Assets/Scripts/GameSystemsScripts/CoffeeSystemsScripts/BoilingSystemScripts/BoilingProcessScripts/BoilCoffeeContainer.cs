using System.Threading;
using SoundsComponentsScripts;
using TweenScripts;
using UnityEngine;

namespace GameSystemsScripts.CoffeeSystemsScripts.BoilingSystemScripts.BoilingProcessScripts
{
    public class BoilCoffeeContainer : MonoBehaviour
    {
        public GameObject BoilGameObject;
        public Transform BoilTransform;
        
        [Space]
        public SpriteRenderer BoilSpriteRenderer;
        public TweenAnimation ActivateBoilTweenGroup;
        public TweenAnimation ProcessBoilTweenGroup;
        
        [Space]
        public SpriteRenderer IndicatorDisplaySpriteRenderer;
        public SpriteRenderer IndicatorArrowSpriteRenderer;
        public Transform IndicatorArrowTransform;
        public TweenAnimation IndicatorShakeTweenGroup;

        [Space]
        public Transform BoildFocusCameraPoint;
        public Transform NormalCameraPoint;

        public SoundContainer SoundContainer;
        
        public CancellationTokenSource ProcessCancellationToken = new CancellationTokenSource();
    }
}
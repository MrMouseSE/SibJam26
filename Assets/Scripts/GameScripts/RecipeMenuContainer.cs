using System.Threading;
using SoundsComponentsScripts;
using TweenScripts;
using UnityEngine;

namespace GameScripts
{
    public class RecipeMenuContainer : MonoBehaviour
    {
        public float Duration;
        public TweenAnimation Animation;

        public AudioContainer Sound;

        public CancellationTokenSource CancelToken = new CancellationTokenSource();

        private void Awake()
        {
            Animation.SetForceState(true);
        }

        private void OnMouseEnter()
        {
            UniTaskAnimationLazyObject animObj = new(Animation, ref CancelToken, Duration, true);
            animObj.Play().Forget();
            Sound.Play(SoundType.AppearSound);
        }

        private void OnMouseExit()
        {
            UniTaskAnimationLazyObject animObj = new(Animation, ref CancelToken, Duration, false);
            animObj.Play().Forget();
            Sound.Play(SoundType.DeathSound);
        }

        private void OnMouseDown()
        {
            Sound.Play(SoundType.ActionSound);
        }
    }
}

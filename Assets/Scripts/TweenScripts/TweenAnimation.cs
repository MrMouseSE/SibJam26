using UnityEngine;

namespace TweenScripts
{
    public class TweenAnimation : MonoBehaviour
    {
        public bool AnimateByUpdate;
        public float Duration;
        public bool IsRandomDelay;
        public Vector2 RandomDelayRange;
        public bool Loop = true;
        
        [Space]
        public AnimationCurve AnimationRule;
        
        private float _currentTime;
        private float _delayTime;

        public void Update()
        {
            if (!AnimateByUpdate) return;
            if (IsRandomDelay)
            {
                _delayTime -= Time.deltaTime;
                if (_delayTime > 0f) return;
            }
            _currentTime -= Time.deltaTime;
            if (_currentTime < 0)
            {
                if (!Loop)
                {
                    AnimateByUpdate = false;
                    return;
                }
                _currentTime = Duration;
                if (IsRandomDelay)
                    SetRandomDelay();
            }
            Evaluate(_currentTime/Duration);
        }

        private void SetRandomDelay()
        {
            _delayTime = Random.Range(RandomDelayRange.x, RandomDelayRange.y);
        }
        
        public virtual void SetForceState(bool isForceStart)
        {}
        
        public virtual void Evaluate(float value)
        { }
        
        protected float GetRuleValue(float progress)
        {
            return AnimationRule.Evaluate(progress);
        }
    }
}
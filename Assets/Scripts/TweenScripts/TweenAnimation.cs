using UnityEngine;

namespace TweenScripts
{
    public class TweenAnimation : MonoBehaviour
    {
        public AnimationCurve AnimationRule;
        
        [Space]
        public bool AnimateByUpdate;
        public float Duration;
        
        private float _currentTime;

        public void Update()
        {
            if (!AnimateByUpdate) return;
            _currentTime -= Time.deltaTime;
            if (_currentTime < 0)
            {
                _currentTime = Duration;
            }
            Evaluate(_currentTime/Duration);
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
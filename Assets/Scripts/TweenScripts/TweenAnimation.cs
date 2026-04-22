using UnityEngine;

namespace TweenScripts
{
    public class TweenAnimation : MonoBehaviour
    {
        public AnimationCurve AnimationRule;
        
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
namespace TweenScripts
{
    public class TweenGroupAnimation : TweenAnimation
    {
        public string TweenGroupName;
        
        public TweenAnimation[] Animations;

        public override void SetForceState(bool isForceStart)
        {
            foreach (var tweenAnimation in Animations)
            {
                tweenAnimation.SetForceState(isForceStart);
            }
        }

        public override void Evaluate(float value)
        {
            foreach (var tweenAnimation in Animations)
            {
                tweenAnimation.Evaluate(value);
            }
        }
    }
}

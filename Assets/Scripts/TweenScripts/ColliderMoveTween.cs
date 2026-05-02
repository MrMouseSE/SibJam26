using UnityEngine;

namespace TweenScripts
{
    public class ColliderMoveTween : TweenAnimation
    {
        public BoxCollider MoveCollider;
        public Vector3 FromPosition;
        public Vector3 ToPosition;

        public override void SetForceState(bool isForceStart)
        {
            SetPosition(isForceStart ? FromPosition : ToPosition);
        }

        public override void Evaluate(float value)
        {
            SetPosition(Vector3.LerpUnclamped(FromPosition, ToPosition, GetRuleValue(value)));
        }

        private void SetPosition(Vector3 position)
        {
            MoveCollider.center = position;
        }

    }
}

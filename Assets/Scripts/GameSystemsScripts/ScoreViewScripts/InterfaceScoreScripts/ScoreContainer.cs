using System.Threading;
using TMPro;
using TweenScripts;
using UnityEngine;

namespace GameSystemsScripts.ScoreViewScripts.InterfaceScoreScripts
{
    public class ScoreContainer : MonoBehaviour
    {
        public TMP_Text ScoreText;
        public TweenAnimation ScoreAnimation;
        public CountTweenAnimation ScoreCountAnimation;
        public float PreviousValue;
        public float CurrentValue;

        public CancellationTokenSource CancToken = new CancellationTokenSource();

        public void SetScoreToAnimation(float value)
        {
            ScoreCountAnimation.FromCount = CurrentValue;
            ScoreCountAnimation.ToCount = value;
            PreviousValue = CurrentValue;
            CurrentValue = value;
        }
    }
}
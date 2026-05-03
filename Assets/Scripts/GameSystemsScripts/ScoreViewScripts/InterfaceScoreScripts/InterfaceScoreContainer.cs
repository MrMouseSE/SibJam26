using System.Collections.Generic;
using UnityEngine;

namespace GameSystemsScripts.ScoreViewScripts.InterfaceScoreScripts
{
    public class InterfaceScoreContainer : MonoBehaviour
    {
        public ScoreContainer RequireScore;
        public ScoreContainer PreviousScoreText;
        public ScoreContainer MaximumScoreText;

        public List<ElementCountPairContainer> CurrentElements;
    }
}
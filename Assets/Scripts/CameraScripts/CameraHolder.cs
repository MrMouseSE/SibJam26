using System.Threading;
using TweenScripts;
using UnityEngine;

namespace CameraScripts
{
    public class CameraHolder : MonoBehaviour
    {
        public Transform CameraHandler;
        public Transform CameraRoot;
        
        public TweenAnimation CameraMoverTween;
        
        public CancellationTokenSource CameraMoverCancellationToken = new CancellationTokenSource();
    }
}
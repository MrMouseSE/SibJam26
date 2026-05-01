using System.Collections.Generic;
using UnityEngine;

namespace GameSystemsScripts.GameInputScripts
{
    public class GameInputComponent
    {
        public bool IsInputLocked;
        public bool MouseWasPressedThisFrame;
        public Vector2 MousePressedPosition;
        public List<Collider> MousePressedHitColliders;
        public bool MouseWasReleasedThisFrame;
        public Vector2 MouseReleasedPosition;
        public List<Collider> MouseReleasedHitColliders;
    }
}
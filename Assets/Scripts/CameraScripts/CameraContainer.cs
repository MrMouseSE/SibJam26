using UnityEngine;

namespace CameraScripts
{
    [RequireComponent(typeof(Camera))]
    public class CameraContainer : MonoBehaviour
    {
        public Camera CameraObject;
        public Transform CameraTransform;

        public void OnValidate()
        {
            if (CameraTransform == null) CameraTransform = transform;
            if (CameraObject == null) CameraObject = GetComponent<Camera>();
        }
    }
}
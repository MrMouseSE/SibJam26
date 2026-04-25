using SoundsComponentsScripts;
using UnityEngine;

namespace GlobalMapScripts
{
    public class PointOfInterestContainer : MonoBehaviour
    {
        public Transform EnvironmentTransform;
        public SpriteRenderer EnvironmentRenderer;
        public ParticleSystem EnvironmentParticleSystem;
        public Collider EnvironmentCollider;
        public AudioContainer AudioContainer; 
    }
}

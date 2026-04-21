using UnityEngine;

namespace SoundsComponentsScripts
{
    public class SoundComponent : MonoBehaviour
    {
        public Transform SoundsTransform;
        public SoundPair[] AppearClips;
        public SoundPair[] ActionClips;
        public SoundPair[] DeathClips;

        private void OnValidate()
        {
            if (SoundsTransform == null)
            {
                SoundsTransform = transform;
            }
        }

        public void PlaySound(SoundType soundType)
        {
            switch (soundType)
            {
                case SoundType.AppearSound:
                    PlayRandomSound(AppearClips);
                    break;
                case SoundType.ActionSound:
                    PlayRandomSound(ActionClips);
                    break;
                case SoundType.DeathSound:
                    PlayRandomSound(DeathClips);
                    break;
            }
        }

        private void PlayRandomSound(SoundPair[] appearClips)
        {
            var clip = appearClips[Random.Range(0, appearClips.Length)].Sound;
            SoundMixerController.PlaySoundAtPosition(SoundsTransform, clip);
        }
    }
}
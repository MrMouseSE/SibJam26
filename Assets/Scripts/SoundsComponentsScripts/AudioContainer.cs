using UnityEngine;

namespace SoundsComponentsScripts
{
    public class AudioContainer : MonoBehaviour
    {
        public Transform AudioTransform;
        
        private void OnValidate()
        {
            if (AudioTransform == null)
                AudioTransform = transform;
        }

        public virtual void Play(SoundType SoundType)
        {
        }
        
        public virtual void Stop()
        {
        }

        protected void PlayRandomSound(SoundPair[] appearClips)
        {
            SoundInstancerController.PlaySoundAtPosition(AudioTransform, GetRandomClip(appearClips));
        }
        
        protected SoundObjectComponent PlayRandomMusic(SoundPair[] musicClips)
        {
            return SoundInstancerController.PlayMusicAtPosition(AudioTransform, GetRandomClip(musicClips));
        }
        
        protected bool StopMusic(SoundObjectComponent soundObject)
        {
            return SoundInstancerController.StopMusic(soundObject);
        }

        private static AudioClip GetRandomClip(SoundPair[] appearClips)
        {
            var clip = appearClips[Random.Range(0, appearClips.Length)].Sound;
            return clip;
        }
    }
}
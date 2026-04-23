using UnityEngine;

namespace SoundsComponentsScripts
{
    [RequireComponent(typeof(MusicContainer))]
    public class AreaMusicContainer : MonoBehaviour
    {
        public MusicContainer MusicContainer;

        public void SwitchMusic(bool active)
        {
            if (active) PlayMusic();
            else StopMusic();
        }

        public void PlayMusic()
        {
            MusicContainer.Play(SoundType.AreaMusic);
        }

        public void StopMusic()
        {
            MusicContainer.Stop();
        }

        private void OnValidate()
        {
            if (MusicContainer == null)
                MusicContainer = GetComponent<MusicContainer>();
        }
    }
}

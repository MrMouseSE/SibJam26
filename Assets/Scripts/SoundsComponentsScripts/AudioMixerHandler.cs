using UnityEngine.Audio;

namespace SoundsComponentsScripts
{
    public class AudioMixerHandler
    {
        private AudioMixer _audioMixer;
        public AudioMixerHandler(AudioMixer gameAudioMixer)
        {
            _audioMixer = gameAudioMixer;
        }

        public void SetVolumeValueByIndex(int index, float volume)
        {
            switch (index)
            {
                case 0:
                    _audioMixer.SetFloat("MasterVolume", volume);
                    break;
                case 1:
                    _audioMixer.SetFloat("SfxVolume", volume);
                    break;
                case 2:
                    _audioMixer.SetFloat("MusicVolume", volume);
                    break;
            }
        }
    }
}

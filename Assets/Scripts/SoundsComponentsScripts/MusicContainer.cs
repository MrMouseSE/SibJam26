using System.Threading;
using Cysharp.Threading.Tasks;

namespace SoundsComponentsScripts
{
    public class MusicContainer : AudioContainer
    {
        public SoundPair[] MusicClips;
        
        private SoundObjectComponent _soundObject;
        private CancellationTokenSource _musicCancellationTokenSource = new CancellationTokenSource();

        public override void Play(SoundType soundType)
        {
            switch (soundType)
            {
                case SoundType.AreaMusic:
                    _soundObject = PlayRandomMusic(MusicClips);
                    _soundObject.OnObjectStop += InvokeCancellation;
                    StartMusicLoop(_soundObject, _musicCancellationTokenSource.Token).Forget();
                    break;
            }
        }

        public override void Stop()
        {
            if (_soundObject == null) return;
            StopMusic(_soundObject);
        }

        private void InvokeCancellation()
        {
            _soundObject.OnObjectStop -= InvokeCancellation;
            _musicCancellationTokenSource.Cancel();
        }

        private async UniTaskVoid StartMusicLoop(SoundObjectComponent musicClip, CancellationToken token)
        {
            
            var length = musicClip.ObjectAudioSource.clip.length;
            await UniTask.WaitForSeconds(length, cancellationToken: token);
            Play(SoundType.AreaMusic);
        }
    }
}
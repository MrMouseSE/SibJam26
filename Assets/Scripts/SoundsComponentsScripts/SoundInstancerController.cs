using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;
using Object = UnityEngine.Object;

namespace SoundsComponentsScripts
{
    public static class SoundInstancerController
    {
        private static readonly List<SoundObjectComponent> _soundObjectsPool = new();
        private static readonly List<SoundObjectComponent> _usedObjectsPool = new();

        private static SoundObjectComponent _refSoundObject;
        private static AudioMixer _gameAudioMixer;

        public static async Task SetRefObjects(AssetReference refObject, AudioMixer gameAudioMixer)
        {
            _gameAudioMixer = gameAudioMixer;
            var handle = Addressables.LoadAssetAsync<GameObject>(refObject);
            await handle;
            _refSoundObject = handle.Result.GetComponent<SoundObjectComponent>();
        }

        public static void FillPool(int poolSize)
        {
            for (int i = 0; i < poolSize; i++)
            {
                _soundObjectsPool.Add(Object.Instantiate(_refSoundObject));
            }
        }
        
        public static void PlaySoundAtPosition(Transform point, AudioClip clip)
        {
            if (_soundObjectsPool.Count < 1)
            {
                FillPool(1);
            }

            var soundObject = _soundObjectsPool[0];
            soundObject.SoundTrasform.position = point.position;
            soundObject.ObjectAudioSource.clip = clip;
            soundObject.ObjectAudioSource.outputAudioMixerGroup = _gameAudioMixer.FindMatchingGroups("SoundsFX")[0];
            soundObject.ObjectAudioSource.Play();
            _soundObjectsPool.Remove(soundObject);
            _usedObjectsPool.Add(soundObject);
            ReturnToPool(soundObject, clip.length).Forget();
        }

        public static SoundObjectComponent PlayMusicAtPosition(Transform point, AudioClip clip)
        {
            if (_soundObjectsPool.Count < 1)
                FillPool(1);
            
            var soundObject = _soundObjectsPool[0];
            soundObject.SoundTrasform.position = point.position;
            soundObject.ObjectAudioSource.clip = clip;
            soundObject.ObjectAudioSource.outputAudioMixerGroup = _gameAudioMixer.FindMatchingGroups("Music")[0];
            soundObject.ObjectAudioSource.Play();
            _soundObjectsPool.Remove(soundObject);
            _usedObjectsPool.Add(soundObject);
            return soundObject;
        }

        public static void StopAllSounds()
        {
            foreach (var objectComponent in _usedObjectsPool)
            {
                StopMusic(objectComponent);
            }
        }

        public static bool StopMusic(SoundObjectComponent soundObject)
        {
            var contains = _usedObjectsPool.Contains(soundObject);
            soundObject.OnObjectStop?.Invoke();
            soundObject.ObjectAudioSource.Stop();
            _usedObjectsPool.Remove(soundObject);
            _soundObjectsPool.Add(soundObject);
            return contains;
        }

        private static async UniTaskVoid ReturnToPool(SoundObjectComponent soundObject, float time)
        {
            await UniTask.WaitForSeconds(time);
            soundObject.ObjectAudioSource.Stop();
            _usedObjectsPool.Remove(soundObject);
            _soundObjectsPool.Add(soundObject);
        }
    }
}

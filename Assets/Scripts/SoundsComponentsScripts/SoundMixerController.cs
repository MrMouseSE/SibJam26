using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace SoundsComponentsScripts
{
    public static class SoundMixerController
    {
        private static readonly List<SoundObjectComponent> _soundObjectsPool = new();
        private static readonly List<SoundObjectComponent> _usedObjectsPool = new();

        private static SoundObjectComponent _refSoundObject;

        public static async void SetRefObject(AssetReference refObject)
        {
            try
            {
                var handle = Addressables.LoadAssetAsync<GameObject>(refObject);
                await handle;
                _refSoundObject = handle.Result.GetComponent<SoundObjectComponent>();
            }
            catch (Exception e)
            {
                Debug.Log("dont load SoundObject");
            }
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
            soundObject.ObjectAudioSource.Play();
            _soundObjectsPool.Remove(soundObject);
            _usedObjectsPool.Add(soundObject);
            ReturnToPool(soundObject, clip.length).Forget();
        }

        private static async UniTaskVoid ReturnToPool(SoundObjectComponent soundObject, float time)
        {
            await UniTask.WaitForSeconds(time);
            _usedObjectsPool.Remove(soundObject);
            _soundObjectsPool.Add(soundObject);
        }
    }
}

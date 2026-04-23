using System.Threading;
using SoundsComponentsScripts;
using TweenScripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MainMenuScripts.SettingsHanlderScripts
{
    public class SettingsHandlerContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public float FoldTime;
        public float UnfoldTime;
        
        public TweenAnimation Animations;

        [Space]
        public Slider MasterSound;
        public Slider SFXSound;
        public Slider MusicSound;
        public AudioMixerHandler Mixer;

        private int _animatingProcessState;
        private CancellationTokenSource _animationCancellationToken = new();

        private void Awake()
        {
            Animations.SetForceState(true);

            SubscribeToValuesChanges();
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_animatingProcessState == 1) return;
            _animatingProcessState = 1;
            StartUniTaskAnimationProcess(UnfoldTime, true);
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            if(_animatingProcessState == -1) return;
            _animatingProcessState = -1;
            StartUniTaskAnimationProcess(FoldTime, false);
        }

        private void StartUniTaskAnimationProcess(float time, bool isForward)
        {
            _animationCancellationToken.Cancel();
            _animationCancellationToken = new CancellationTokenSource();
            UniTaskAnimationObject uniTaskAnimationObject = new();
            uniTaskAnimationObject.StartAnimation(Animations, _animationCancellationToken.Token, time, isForward).Forget();
            uniTaskAnimationObject.AnimationCompleted += ResetState;
        }

        private void ResetState(UniTaskAnimationObject uniTaskAnimationObject)
        {
            uniTaskAnimationObject.AnimationCompleted -= ResetState;
            _animatingProcessState = 0;
        }

        private void OnDestroy()
        {
            UnsubscribeFromValuesChanges();
        }

        private void SubscribeToValuesChanges()
        {
            MasterSound.onValueChanged.AddListener(SetMasterVolume);
            SFXSound.onValueChanged.AddListener(SetSfxVolume);
            MusicSound.onValueChanged.AddListener(SetMusicVolume);
        }

        private void SetMasterVolume(float volume)
        {
            Mixer.SetVolumeValueByIndex(0, volume);
        }
        
        private void SetSfxVolume(float volume)
        {
            Mixer.SetVolumeValueByIndex(1, volume);
        }

        private void SetMusicVolume(float volume)
        {
            Mixer.SetVolumeValueByIndex(2, volume);
        }

        private void UnsubscribeFromValuesChanges()
        {
            MasterSound.onValueChanged.RemoveAllListeners();
            SFXSound.onValueChanged.RemoveAllListeners();
            MusicSound.onValueChanged.RemoveAllListeners();
        }
    }
}

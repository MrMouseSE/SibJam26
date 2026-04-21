using System;
using SoundsComponentsScripts;
using UnityEngine;

namespace MainMenuScripts
{
    public class MenuButtonContainer : MonoBehaviour
    {
        public Animation AnimationComponent;
        public AnimationClip PushClip;
        public AnimationClip OverClip;
        public AnimationClip PushUpClip;

        [Space]
        public SoundComponent ButtonSounds;
        
        public Action OnButtonPushed;
        
        private bool _isButtonPushed = false;
    
        private void OnMouseEnter()
        {
            if(_isButtonPushed) return;
            AnimationComponent.Play(OverClip.name);
            ButtonSounds.PlaySound(SoundType.ActionSound);
        }

        private void OnMouseExit()
        {
            if (_isButtonPushed) return;
            StopAndRewind();
        }

        private void OnMouseDown()
        {
            _isButtonPushed = true;
            StopAndRewind();
            AnimationComponent.Play(PushClip.name);
            ButtonSounds.PlaySound(SoundType.AppearSound);
            OnButtonPushed?.Invoke();
        }

        private void OnMouseUp()
        {
            _isButtonPushed = false;
            StopAndRewind();
            AnimationComponent.Play(PushUpClip.name);
            ButtonSounds.PlaySound(SoundType.DeathSound);
        }

        private void StopAndRewind()
        {
            AnimationComponent.Stop();
            AnimationComponent.Rewind();
        }
    }
}

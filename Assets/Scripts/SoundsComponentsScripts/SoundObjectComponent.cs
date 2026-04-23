using System;
using UnityEngine;

namespace SoundsComponentsScripts
{
    public class SoundObjectComponent : MonoBehaviour
    {
        public Transform SoundTrasform;
        public AudioSource ObjectAudioSource;

        public Action OnObjectStop;
    }
}
using System;
using SoundsComponentsScripts;
using TMPro;
using UnityEngine;

namespace MainMenuScripts
{
    public class MainMenuEnvironmentContainer : MonoBehaviour
    {
        public DescriptionEnviromentObjectText DescriptionEnviromentObjectText;
        
        public TMP_Text DescriptionText;
        public AudioContainer AudioContainer;

        public void Awake()
        {
            DescriptionText.text = "";
        }

        public void OnMouseEnter()
        {
            AudioContainer.Play(SoundType.ActionSound);
            DescriptionText.text = DescriptionEnviromentObjectText.Description;
        }

        public void OnMouseExit()
        {
            DescriptionText.text = "";
        }
    }
}

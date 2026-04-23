using SoundsComponentsScripts;
using UnityEngine;

namespace MainMenuScripts
{
    public class MainMenuEnvironmentContainer : MonoBehaviour
    {
        public AudioContainer AudioContainer;

        public void OnMouseEnter()
        {
            AudioContainer.Play(SoundType.ActionSound);
        }
    }
}

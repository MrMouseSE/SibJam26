using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MainMenuScripts.SettingsHanlderScripts
{
    public class ExitFromSettingsContainer : MonoBehaviour
    {
        public void ExitFromApplication()
        {
            Exit().Forget();
        }

        private async UniTask Exit()
        {
            await UniTask.WaitForSeconds(0.25f);
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }
}
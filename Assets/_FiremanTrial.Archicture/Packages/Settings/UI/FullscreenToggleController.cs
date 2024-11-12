using UnityEngine;
using UnityEngine.UI;

namespace FiremanTrial.Settings.UI
{
    public class FullscreenToggleController : MonoBehaviour
    {
        [SerializeField] private  Toggle fullscreenToggle;
        private const string FullscreenPrefKey = "FullscreenSetting";

        private void Start()
        {
            var isFullscreen = PlayerPrefs.GetInt(FullscreenPrefKey, Screen.fullScreen ? 1 : 0) == 1;
            Screen.fullScreen = isFullscreen;
            fullscreenToggle.isOn = Screen.fullScreen;
            fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        }
        
        private void SetFullscreen(bool isFullscreen) 
        {
            Screen.fullScreen = isFullscreen;
            PlayerPrefs.SetInt(FullscreenPrefKey, isFullscreen ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
}

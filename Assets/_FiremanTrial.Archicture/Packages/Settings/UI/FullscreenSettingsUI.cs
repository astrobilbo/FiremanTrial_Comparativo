using UnityEngine;
using UnityEngine.UI;

namespace FiremanTrial.Settings.UI
{
    public class FullscreenSettingsUI : MonoBehaviour
    {
        [SerializeField] private Settings settings;
        [SerializeField] private  Toggle fullscreenToggle;
        
        private void OnEnable() => settings.OnFullScreenChanged += ToggleIsOn;

        private void OnDisable() => settings.OnFullScreenChanged -= ToggleIsOn;

        private void Awake() => Initialize();

        private void Initialize()
        {
            fullscreenToggle.isOn = Screen.fullScreen;
            fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        }

        private void ToggleIsOn(bool isOn)
        {
            fullscreenToggle.isOn = isOn;
        }
        
        private void SetFullscreen(bool isFullscreen) 
        {
            settings.ChangeFullScreen(isFullscreen);
        }
    }
}

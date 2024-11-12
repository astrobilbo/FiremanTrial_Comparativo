using UnityEngine;
using UnityEngine.UI;

namespace FiremanTrial.Settings.UI
{
    public class VsyncToggleController : MonoBehaviour
    {
        [SerializeField] private Toggle vsyncToggle;
        private const string VSyncPrefKey = "VSyncSetting";

        private void Start()
        {
            bool isVSyncEnabled = PlayerPrefs.GetInt(VSyncPrefKey, 0) == 1;
            vsyncToggle.isOn = isVSyncEnabled;
            QualitySettings.vSyncCount = isVSyncEnabled ? 1 : 0;
            vsyncToggle.onValueChanged.AddListener(SetVSync);
            
        }

        public void VsyncUpdated()
        {
            bool isVSyncEnabled = QualitySettings.vSyncCount == 1;
            vsyncToggle.isOn = isVSyncEnabled;
        }
        private void SetVSync(bool isEnabled)
        {
            // Set the VSync count based on the toggle state
            QualitySettings.vSyncCount = isEnabled ? 1 : 0;
            PlayerPrefs.SetInt(VSyncPrefKey, isEnabled ? 1 : 0); // Save setting
            PlayerPrefs.Save(); // Ensure preferences are saved
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

namespace FiremanTrial.Settings.UI
{
    public class VsyncSettingsUI : MonoBehaviour
    {
        [SerializeField] private Settings settings;
        [SerializeField] private Toggle vsyncToggle;
        private bool _value;
        
        private void OnEnable() => settings.OnVSyncChanged += VsyncToggle;

        private void OnDisable() => settings.OnVSyncChanged -= VsyncToggle;

        private void Awake() => vsyncToggle.onValueChanged.AddListener(SetVSync);

        private void VsyncToggle(int value)
        {
            _value= value != 0;
            vsyncToggle.isOn = enabled;
        }
        private void SetVSync(bool isEnabled)
        {
            if (_value == isEnabled) return;
            settings.ChangeVSync(isEnabled?1:0);
        }
    }
}

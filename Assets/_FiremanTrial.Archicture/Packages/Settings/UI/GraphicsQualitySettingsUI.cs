using TMPro;
using UnityEngine;

namespace FiremanTrial.Settings.UI
{
    public class GraphicsQualitySettingsUI : MonoBehaviour
    {
        [SerializeField] private Settings settings;
        [SerializeField] private TMP_Dropdown qualityDropdown;
        int _activeQuality;
        private void OnEnable() => settings.OnGraphicsQualityChanged += RefreshDropdown;

        private void OnDisable() => settings.OnGraphicsQualityChanged -= RefreshDropdown;

        private void Awake() => Initialize();

        private void Initialize()
        {
            qualityDropdown.options.Clear();
            foreach (var qualitySettingName in QualitySettings.names)
            {
                qualityDropdown.options.Add(new TMP_Dropdown.OptionData(qualitySettingName));
            }
            qualityDropdown.onValueChanged.AddListener(ChangeQuality);
        }
        
        private void RefreshDropdown(int quality)
        {
            _activeQuality=quality;
            qualityDropdown.value =_activeQuality;
            qualityDropdown.RefreshShownValue();
        }
        
        private void ChangeQuality(int index)
        {
            if (index==_activeQuality) return;
            settings.ChangeGraphicsQuality(index);
        }
        
    }
}

using TMPro;
using UnityEngine;

namespace FiremanTrial.UI
{
    public class DropdownGraphicsQualityController : MonoBehaviour
    {
        [SerializeField] private VsyncToggleController vsyncToggleController;

        [SerializeField] private TMP_Dropdown qualityDropdown;
        private const string QualityPrefKey = "QualitySetting";

        private void Start()
        {
            var savedQualityLevel = PlayerPrefs.GetInt(QualityPrefKey, QualitySettings.GetQualityLevel());
            QualitySettings.SetQualityLevel(savedQualityLevel, true);
            
            qualityDropdown.options.Clear();
            foreach (var qualitySettingName in QualitySettings.names)
            {
                qualityDropdown.options.Add(new TMP_Dropdown.OptionData(qualitySettingName));
            }
            
            qualityDropdown.value = QualitySettings.GetQualityLevel();
            qualityDropdown.RefreshShownValue();
            qualityDropdown.onValueChanged.AddListener(ChangeQuality);
        }

        private void ChangeQuality(int index)
        {
            QualitySettings.SetQualityLevel(index);
            vsyncToggleController?.VsyncUpdated();

            PlayerPrefs.SetInt(QualityPrefKey, index);
            PlayerPrefs.Save();
        }
        
    }
}

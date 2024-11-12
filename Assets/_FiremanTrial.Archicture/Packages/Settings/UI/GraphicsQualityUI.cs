using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FiremanTrial.Settings.UI
{
    public class GraphicsQualityUI : MonoBehaviour
    {
        [SerializeField] private Settings settings;
        [SerializeField] private TMP_Dropdown qualityDropdown;
        
        private void OnEnable()
        {
            settings.OnGraphicsQualityChanged += RefreshDropdown;
        }

        private void OnDisable()
        {
            settings.OnGraphicsQualityChanged -= RefreshDropdown;
        }

        private void Awake()
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
            qualityDropdown.value =quality;
            qualityDropdown.RefreshShownValue();
        }
        private void ChangeQuality(int index)
        {
            settings.ChangeGraphicsQuality(index,gameObject);
        }
        
    }
}

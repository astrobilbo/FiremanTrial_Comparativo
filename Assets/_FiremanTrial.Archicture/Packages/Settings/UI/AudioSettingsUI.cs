using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace FiremanTrial.Settings.UI
{
    public class AudioSettingsUI : MonoBehaviour
    {
        [SerializeField] private Settings settings;
        [SerializeField] private  Slider slider;
        [SerializeField] private  string soundKey;
        private const float SliderMin = 0f;
        private const float SliderMax = 20f;
        private float _volume;
        private void OnEnable() => settings.OnVolumeChanged += ChangeSliderValue;

        private void OnDisable() => settings.OnVolumeChanged -= ChangeSliderValue;

        private void Awake() => Initialize();

        private void Initialize()
        {
            SetSliderRange();
            slider.wholeNumbers = true;
            slider.onValueChanged.AddListener(ChangeVolume);
        }

        private void SetSliderRange()
        {
            slider.minValue = SliderMin;
            slider.maxValue= SliderMax;
        }

        private void ChangeSliderValue(string key, float value)
        {
            if (key != soundKey || Mathf.Approximately(_volume, value)) return;
            
            _volume = value;
            slider.value = _volume;
        }

        private void ChangeVolume(float value)
        {
            if (Mathf.Approximately(_volume, value)) return;
            settings.ChangeVolume(soundKey, value);
        }
    }
}

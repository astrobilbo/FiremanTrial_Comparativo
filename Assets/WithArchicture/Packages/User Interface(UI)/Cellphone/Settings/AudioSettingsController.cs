using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace FiremanTrial.UI
{
    public class AudioSettingsController : MonoBehaviour
    {
        [SerializeField] private  AudioMixer mixer;

        [SerializeField] private  Slider slider;
        [SerializeField] private  string soundKey;
        [SerializeField,Range(0,10)] private  float initialVolume= 5f;
        private const float MinDb = -80f;
        private const float MaxDb = 0f;
        private const float SliderMin = 0f;
        private const float SliderMax = 10f;
        private void Start()
        {
            SetSliderRange();
            slider.wholeNumbers = true;
            var savedVolume = PlayerPrefs.GetFloat(soundKey,initialVolume);
            mixer.SetFloat(soundKey,DBVolume(savedVolume));
            slider.value = savedVolume;
            slider.onValueChanged.AddListener(SetVolume);
        }

        private void SetSliderRange()
        {
            slider.minValue = SliderMin;
            slider.maxValue= SliderMax;
        }

        private void SetVolume(float volume)
        {
            mixer.SetFloat(soundKey,DBVolume(volume));
            PlayerPrefs.SetFloat(soundKey, volume);
            PlayerPrefs.Save();

        }

        private float DBVolume(float volume) => Mathf.Lerp(MinDb, MaxDb, volume / SliderMax);
    }

}

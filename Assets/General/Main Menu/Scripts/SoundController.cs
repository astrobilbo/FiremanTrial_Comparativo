using TMPro;
using UnityEngine;
using UnityEngine.Audio;

namespace FiremanTrial.MainMenu
{
    public class SoundController : MonoBehaviour
    {
        [SerializeField] private TMP_InputField geralSoundTMPInputField;
        [SerializeField] private  TMP_InputField musicSoundTMPInputField;
        [SerializeField] private  TMP_InputField fxSoundTMPInputField;
        
        [SerializeField] private  AudioMixer mixer;
        
        private const string GeralKey = "Geral";
        private const string MusicKey = "Music";
        private const string FXKey = "FX";
        private const int DefaultVolume = 80;

        void Start()
        {
                InitializeSound(geralSoundTMPInputField, GeralKey, SetGeralVolume);
                InitializeSound(musicSoundTMPInputField, MusicKey, SetMusicVolume);
                InitializeSound(fxSoundTMPInputField, FXKey, SetFXVolume);
        }

        private void InitializeSound(TMP_InputField inputField, string key, System.Action<string> setVolumeAction)
        {
            string savedVolume = PlayerPrefs.GetString(key, DefaultVolume.ToString());
            inputField.placeholder.GetComponent<TextMeshProUGUI>().text = savedVolume;
            setVolumeAction(savedVolume);
        }
     

        public void SetGeralVolume(string value) => SetVolume(value, geralSoundTMPInputField, GeralKey, "Master");
        public void SetMusicVolume(string value) => SetVolume(value, musicSoundTMPInputField, MusicKey, "Music");
        public void SetFXVolume(string value) => SetVolume(value, fxSoundTMPInputField, FXKey, "FX");
        private void SetVolume(string value, TMP_InputField inputField, string key, string mixerParameter)
        {
            if (!int.TryParse(value, out int newVolume))
            {
                ResetInputField(inputField, key);
                return;
            }

            newVolume = Mathf.Clamp(newVolume, 0, 100);
            inputField.text = "";
            inputField.placeholder.GetComponent<TextMeshProUGUI>().text = newVolume.ToString();
            mixer.SetFloat(mixerParameter, newVolume-80);
            PlayerPrefs.SetString(key, newVolume.ToString());
            PlayerPrefs.Save();
        }
        
        private void ResetInputField(TMP_InputField inputField, string key)
        {
            inputField.text = "";
            inputField.placeholder.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString(key, DefaultVolume.ToString());
        }
    }
}

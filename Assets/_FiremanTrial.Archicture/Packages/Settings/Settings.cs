using System;
using UnityEngine.Audio;
using UnityEngine;
using FiremanTrial.PermanentData;

namespace FiremanTrial.Settings
{
    public class Settings: MonoBehaviour
    {
        [SerializeField] private AudioMixer mixer;
        
        private const float MinDb = -80f;
        private const float MaxDb = 0f;
        
        private const string MasterSoundKey = "MasterSound";
        private const string MusicSoundKey = "MusicSound";
        private const  string FXSoundKey= "fxSound";  
        private const  string GraphicsQualityKey= "graphicsQuality";
        private const  string FullScreenKey= "fullScreen";
        private const  string VSyncKey= "vSync";
        
        private float _masterSoundVolume= 5f;
        private float _musicSoundVolume= 5f;
        private float _fxSoundVolume= 5f;
        private int _graphicsQuality;
        private int _vSync;
        private bool _fullScreen;

        public Action<string,float> OnVolumeChanged;
        public Action<int> OnGraphicsQualityChanged;
        public Action<int> OnVSyncChanged;
        public Action<bool> OnFullScreenChanged;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
           LoadSettings();
           TriggerSettingCallbacks();
           ApplyInitialSettings();
        }
        
        private void LoadSettings()
        {
            _masterSoundVolume= PlayerPrefsData.LoadData(MasterSoundKey, _masterSoundVolume);
            _musicSoundVolume= PlayerPrefsData.LoadData(MusicSoundKey, _musicSoundVolume);
            _fxSoundVolume= PlayerPrefsData.LoadData(FXSoundKey, _fxSoundVolume);
            _graphicsQuality= PlayerPrefsData.LoadData(GraphicsQualityKey, QualitySettings.GetQualityLevel());
            _fullScreen= PlayerPrefsData.LoadData(FullScreenKey, Screen.fullScreen);
            _vSync= PlayerPrefsData.LoadData(VSyncKey, QualitySettings.vSyncCount);
        }
        
        private void TriggerSettingCallbacks()
        {
            OnVolumeChanged?.Invoke(MasterSoundKey,_masterSoundVolume); 
            OnVolumeChanged?.Invoke(MusicSoundKey,_musicSoundVolume);
            OnVolumeChanged?.Invoke(FXSoundKey,_fxSoundVolume);
            OnGraphicsQualityChanged?.Invoke(_graphicsQuality);
            OnFullScreenChanged.Invoke(_fullScreen);
            OnVSyncChanged?.Invoke(_vSync);
        }
        
        private void ApplyInitialSettings()
        {
            mixer.SetFloat(MasterSoundKey,DBVolume(_masterSoundVolume));
            mixer.SetFloat(MusicSoundKey,DBVolume(_musicSoundVolume));
            mixer.SetFloat(FXSoundKey,DBVolume(_fxSoundVolume));
            QualitySettings.SetQualityLevel(_graphicsQuality);
        }
        
        public void ChangeVolume(string key, float volume)
        {
            switch (key)
            {
                case MasterSoundKey when !Mathf.Approximately(volume, _masterSoundVolume):
                    SetVolume(MasterSoundKey, _masterSoundVolume = volume);
                    break;
                case MusicSoundKey when !Mathf.Approximately(volume, _musicSoundVolume):
                    SetVolume(MusicSoundKey, _musicSoundVolume = volume);
                    break;
                case FXSoundKey when !Mathf.Approximately(volume, _fxSoundVolume):
                    SetVolume(FXSoundKey, _fxSoundVolume = volume);
                    break;
                default:
                    Debug.LogError($"Key {key} not found");
                    break;
            }
        }

        private void SetVolume(string key, float volume)
        {
            mixer.SetFloat(key, DBVolume(volume));
            PlayerPrefsData.SaveData(key, volume);
            OnVolumeChanged?.Invoke(key, volume);
        }
        
        private static float DBVolume(float volume) => Mathf.Lerp(MinDb, MaxDb, volume / 20f); //20 is the audio slider max value (range from 0 to 20)

        public void ChangeGraphicsQuality(int quality,GameObject caller)
        {
            Debug.Log($"The graphics is being changed by the {caller.name}", caller);
            _graphicsQuality = quality;
            QualitySettings.SetQualityLevel(_graphicsQuality);
            PlayerPrefsData.SaveData(GraphicsQualityKey, _graphicsQuality);
            OnGraphicsQualityChanged?.Invoke(_graphicsQuality);
            OnVSyncChanged?.Invoke(QualitySettings.vSyncCount);
        }
    }
}
using System;
using UnityEngine.Audio;
using UnityEngine;
using FiremanTrial.PermanentData;

namespace FiremanTrial.Settings
{
    public class Settings: MonoBehaviour
    {
        [SerializeField] private AudioMixer mixer;
        
        public Action<string,float> OnVolumeChanged;
        public Action<int> OnGraphicsQualityChanged;
        public Action<int> OnVSyncChanged;
        public Action<bool> OnFullScreenChanged;

        private void Start() => Initialize();

        private void Initialize()
        {
           LoadSettings();
           TriggerSettingCallbacks();
           ApplyInitialSettings();
        }
        
        private void LoadSettings()
        {
            SettingsData.MasterSoundVolume= PlayerPrefsData.LoadData(SettingsData.MasterSoundKey, SettingsData.MasterSoundVolume);
            SettingsData.MusicSoundVolume= PlayerPrefsData.LoadData(SettingsData.MusicSoundKey,SettingsData.MusicSoundVolume);
            SettingsData.FXSoundVolume= PlayerPrefsData.LoadData(SettingsData.FXSoundKey,SettingsData. FXSoundVolume);
            SettingsData.GraphicsQuality= PlayerPrefsData.LoadData(SettingsData.GraphicsQualityKey, QualitySettings.GetQualityLevel());
            SettingsData.FullScreen= PlayerPrefsData.LoadData(SettingsData.FullScreenKey, Screen.fullScreen);
            SettingsData.VSync= PlayerPrefsData.LoadData(SettingsData.VSyncKey, QualitySettings.vSyncCount);
        }
        
        private void TriggerSettingCallbacks()
        {
            OnVolumeChanged?.Invoke(SettingsData.MasterSoundKey,SettingsData.MasterSoundVolume); 
            OnVolumeChanged?.Invoke(SettingsData.MusicSoundKey,SettingsData.MusicSoundVolume);
            OnVolumeChanged?.Invoke(SettingsData.FXSoundKey,SettingsData.FXSoundVolume);
            OnGraphicsQualityChanged?.Invoke(SettingsData.GraphicsQuality);
            OnFullScreenChanged.Invoke(SettingsData.FullScreen);
            OnVSyncChanged?.Invoke(SettingsData.VSync);
        }
        
        private void ApplyInitialSettings()
        {
            mixer.SetFloat(SettingsData.MasterSoundKey,DBVolume(SettingsData.MasterSoundVolume));
            mixer.SetFloat(SettingsData.MusicSoundKey,DBVolume(SettingsData.MusicSoundVolume));
            mixer.SetFloat(SettingsData.FXSoundKey,DBVolume(SettingsData.FXSoundVolume));
            QualitySettings.SetQualityLevel(SettingsData.GraphicsQuality);
        }
        
        public void ChangeVolume(string key, float volume)
        {
            switch (key)
            {
                case SettingsData.MasterSoundKey when !Mathf.Approximately(volume, SettingsData.MasterSoundVolume):
                    SetVolume(SettingsData.MasterSoundKey, SettingsData.MasterSoundVolume = volume);
                    break;
                case SettingsData.MusicSoundKey when !Mathf.Approximately(volume, SettingsData.MusicSoundVolume):
                    SetVolume(SettingsData.MusicSoundKey, SettingsData.MusicSoundVolume = volume);
                    break;
                case SettingsData.FXSoundKey when !Mathf.Approximately(volume, SettingsData.FXSoundVolume):
                    SetVolume(SettingsData.FXSoundKey, SettingsData.FXSoundVolume = volume);
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
        
        private static float DBVolume(float volume) => Mathf.Lerp(SettingsData.MinDb, SettingsData.MaxDb, volume / 20f);

        public void ChangeGraphicsQuality(int quality)
        {
            SettingsData.GraphicsQuality = quality;
            QualitySettings.SetQualityLevel(SettingsData.GraphicsQuality);
            PlayerPrefsData.SaveData(SettingsData.GraphicsQualityKey, SettingsData.GraphicsQuality);
            OnGraphicsQualityChanged?.Invoke(SettingsData.GraphicsQuality);
            OnVSyncChanged?.Invoke(QualitySettings.vSyncCount);
        }

        public void ChangeFullScreen(bool isFullScreen)
        {
            SettingsData.FullScreen = isFullScreen;
            Screen.fullScreen = SettingsData.FullScreen;
            PlayerPrefsData.SaveData(SettingsData.FullScreenKey, SettingsData.FullScreen);
            OnFullScreenChanged?.Invoke(SettingsData.FullScreen);
        }

        public void ChangeVSync(int value)
        {
            SettingsData.VSync = value;
            QualitySettings.vSyncCount=SettingsData.VSync;
             PlayerPrefsData.SaveData(SettingsData.VSyncKey, SettingsData.VSync);
             OnVSyncChanged?.Invoke(QualitySettings.vSyncCount);
        }
    }
}
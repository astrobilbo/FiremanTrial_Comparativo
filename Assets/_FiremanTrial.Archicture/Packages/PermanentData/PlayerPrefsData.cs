using UnityEngine;

namespace FiremanTrial.PermanentData
{
    public static class PlayerPrefsData
    {
        public static void SaveData(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefs.Save();
        }
        public static void SaveData(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }
        public static void SaveData(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }
        public static void SaveData(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
            PlayerPrefs.Save();
        }
        public static float LoadData(string key, float value)
        {
            return PlayerPrefs.GetFloat(key, value);
        }
        public static int LoadData(string key, int value)
        {
            return PlayerPrefs.GetInt(key, value);
        }
        public static string LoadData(string key, string value)
        {
            return PlayerPrefs.GetString(key, value);
        }
        public static bool LoadData(string key, bool value)
        {
            return PlayerPrefs.GetInt(key, value ? 1 : 0) == 1;
        }
        public static void ClearData(string key)
        {
            PlayerPrefs.DeleteKey(key);
            PlayerPrefs.Save();
        }
        public static void ClearAllData()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
        
}
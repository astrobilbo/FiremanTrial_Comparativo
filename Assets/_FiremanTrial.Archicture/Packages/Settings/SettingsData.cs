namespace FiremanTrial.Settings
{
    public static class SettingsData
    {
        public const float MinDb = -80f;
        public const float MaxDb = 0f;
        
        public const string MasterSoundKey = "MasterSound";
        public const string MusicSoundKey = "MusicSound";
        public const  string FXSoundKey= "FXSound";  
        public const  string GraphicsQualityKey= "graphicsQuality";
        public const  string FullScreenKey= "fullScreen";
        public const  string VSyncKey= "VSync";
        
        public static float MasterSoundVolume= 5f;
        public static float MusicSoundVolume= 5f;
        public static float FXSoundVolume= 5f;
        public static int GraphicsQuality;
        public static int VSync;
        public static bool FullScreen;
    }
}
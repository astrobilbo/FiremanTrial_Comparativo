using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FiremanTrial.MainMenu
{
    public class GraphicsControll : MonoBehaviour
    {
        public Toggle fullScreen;
        public TMP_Dropdown graphics;

        void Awake()
        {
            fullScreen.isOn = Screen.fullScreen;
            List<string> options = new List<string> { "Baixo", "Medio", "Alto", "Ultra" };
            graphics.ClearOptions();
            graphics.AddOptions(options);
            graphics.value = QualitySettings.GetQualityLevel();
            graphics.RefreshShownValue();

        }

        public void SetFullScreen(bool newScreenValue) 
        {
            Screen.fullScreen = newScreenValue;
        }
        public void SetGraphics(int newGraphicsValue)
        {
            QualitySettings.SetQualityLevel(newGraphicsValue);
        }
    }
}

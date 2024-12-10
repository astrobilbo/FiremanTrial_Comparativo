using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FiremanTrial.General.Canvas
{
    public class InfoText : MonoBehaviour
    {
        [SerializeField] List<string> textOptions;
        [SerializeField] TextMeshProUGUI textMeshProUGUI;
        private void Start()
        {
            string selectedText = textOptions[Random.Range(0, textOptions.Count)];

            selectedText = selectedText.Replace("\\n", "\n");

            textMeshProUGUI.text = selectedText;
        }
    }
}

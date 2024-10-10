using System.Collections;
using TMPro;
using UnityEngine;

namespace FiremanTrial
{
    public class FPSCounter : MonoBehaviour
    {    
        [SerializeField] private TextMeshProUGUI fpsCounterText;
        [SerializeField] private bool fpsEnabled = true;
        [SerializeField] private float fpsCounterDisplayRate = 0.5f;
        
        private WaitForSeconds _waitForSeconds;
        private void Start()
        {
            _waitForSeconds = new WaitForSeconds(fpsCounterDisplayRate);
            if (CanDisplayFPS())
            { 
                StartCoroutine(UpdateFPSCounter());
            }
        }

        private bool CanDisplayFPS()
        {
            var canDisplay = true;
        
            if (fpsCounterText is null)
            {
                Debug.Log("FPS Counter Text is null");
                canDisplay = false;
            }
            else if (!fpsEnabled)
            {
                Debug.Log("FPS Counter Disabled");
                canDisplay = false;
            }
            return canDisplay;
        }

        private IEnumerator UpdateFPSCounter()
        {
            while (fpsEnabled)
            {
                fpsCounterText.text = $"FPS: {(int)(1f / Time.unscaledDeltaTime)}";
                yield return _waitForSeconds;
            }
        }
    }
}
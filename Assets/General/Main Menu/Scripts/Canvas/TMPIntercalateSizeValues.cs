using System.Collections;
using TMPro;
using UnityEngine;

namespace FiremanTrial.MainMenu.Canvas
{
    public class TMPIntercalateSizeValues : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI textMeshProUGUI;
        [SerializeField] float smallSize;
        [SerializeField] float largeSize;
        [SerializeField] float duration;

        private void Start()
        {
            StartCoroutine(Variation());
        }
        IEnumerator Variation()
        {
            while (true)
            {
                textMeshProUGUI.fontSize = CalculateInterpolatedSize();
                yield return null;
            }
        }
        private float CalculateInterpolatedSize()
        {
            return smallSize + (Mathf.Sin(Time.time * duration) + 1f) / 2f * (largeSize - smallSize);
        }
    }
}

using UnityEngine;

namespace FiremanTrial.General.Canvas
{
    public class SetCanvasGroup : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        private void Start()
        {
            if (canvasGroup == null)
            {
                if (!TryGetComponent<CanvasGroup>(out canvasGroup))
                {
                    canvasGroup = gameObject.AddComponent<CanvasGroup>();
                }
            }
        }
        public void ToggleVisibility() => CanvasGroupManager.Visible(!canvasGroup.blocksRaycasts, canvasGroup);
        public void SetVisibility(bool value) => CanvasGroupManager.Visible(value, canvasGroup);
    }
}

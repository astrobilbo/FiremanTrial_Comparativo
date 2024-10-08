using UnityEngine;

namespace FiremanTrial.MainMenu
{
    public static class CanvasGroupManager
    {
        public static void Visible(bool b, CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = b ? 1 : 0;
        }
    }
}

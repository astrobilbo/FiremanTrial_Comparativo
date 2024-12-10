using System;
using UnityEngine;

namespace FiremanTrial.General
{
    [Serializable]
    public static class CanvasGroupManager
    {
        public static void Visible(bool active, CanvasGroup obj)
        {
            if ( obj is null)
            {
                Debug.LogWarning("CanvasGroup is null!");
                return;
            }
            obj.alpha = active ? 1 : 0;
            obj.blocksRaycasts = active;
            obj.interactable = active;
        }
    }
}

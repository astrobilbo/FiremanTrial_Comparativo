using UnityEngine;

namespace FiremanTrial.UI
{
    public class AppSettings : MonoBehaviour
    {
        [SerializeField] private Cellphone cellphone;
        [SerializeField] private CanvasGroup appCanvasGroup;
        
        
        public void Open()
        {
            cellphone.OpenApp(appCanvasGroup);    
        }
    }
}
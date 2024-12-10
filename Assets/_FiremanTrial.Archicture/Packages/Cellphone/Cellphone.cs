using System.Collections.Generic;
using FiremanTrial.General;
using UnityEngine;
using UnityEngine.UI;

namespace FiremanTrial.UI
{
    public class Cellphone : MonoBehaviour
    {
        [SerializeField] private CanvasGroup cellphoneCanvasGroup;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private List<CanvasGroup> appsCanvasGroup;
        [SerializeField] private CanvasGroup mainMenuCanvasGroup;
        bool inMainMenu;

        private void Start()
        {
            inMainMenu= mainMenuCanvasGroup.blocksRaycasts ;
        }

        public void ChangeBackgroundImage(Sprite sprite, Color color)
        {
            backgroundImage.sprite = sprite;
            backgroundImage.color = color;
        }

        public void GoToMainMenu()
        {
            CanvasGroupManager.Visible(true,mainMenuCanvasGroup);
            inMainMenu = true;
            foreach (var appCanvaGroup in appsCanvasGroup)
            {
                CanvasGroupManager.Visible(false,appCanvaGroup);
            }
        }

        public void OpenApp(CanvasGroup canvasGroup)
        {
            if (inMainMenu)
            {
                GoToMainMenu();
                CanvasGroupManager.Visible(false, mainMenuCanvasGroup);
            }

            inMainMenu = false;
            CanvasGroupManager.Visible(false,canvasGroup);
        }
        public void TurnOnOffCellphone()
        {
            CanvasGroupManager.Visible(!cellphoneCanvasGroup.blocksRaycasts,cellphoneCanvasGroup);
        }
        
    }
}


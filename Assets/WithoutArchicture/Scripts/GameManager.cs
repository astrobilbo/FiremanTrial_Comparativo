using UnityEngine;

namespace FiremanTrial.WithoutArchitecture
{
    public class GameManager : MonoBehaviour
    {
        
        [SerializeField] private Player player;

        [SerializeField] private CanvasGroup cellphone;
        private bool _mouseLocked;
        // Start is called before the first frame update
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _mouseLocked = true;
            player.CanMove = true;
            cellphone.alpha = 0;
            cellphone.interactable = false;
            cellphone.blocksRaycasts = false;
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                if (_mouseLocked)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    player.CanMove = false;
                    _mouseLocked = false;
                    cellphone.alpha = 1;
                    cellphone.interactable = true;
                    cellphone.blocksRaycasts = true;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    player.CanMove = true;
                    _mouseLocked = true;
                    cellphone.alpha = 0;
                    cellphone.interactable = false;
                    cellphone.blocksRaycasts = false;
                }
            }    
        }

        public void LockMouse()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            player.CanMove = true;
            _mouseLocked = true;
            cellphone.alpha = 0;
            cellphone.interactable = false;
            cellphone.blocksRaycasts = false;
        }
        public void WinGame()
        {
            Debug.Log("Victory!");
        }
    }
}

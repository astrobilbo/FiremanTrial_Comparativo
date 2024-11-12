using UnityEngine;

namespace FiremanTrial.WithArchitecture
{
    public class MouseLockToggle : MonoBehaviour
    {
        public void UpdateCursorState(bool lockState)
        {
            Cursor.lockState = lockState ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !lockState;
        }
    }
}

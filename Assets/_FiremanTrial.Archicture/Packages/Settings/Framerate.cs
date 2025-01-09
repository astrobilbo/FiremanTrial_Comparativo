using UnityEngine;

namespace FiremanTrial.Settings
{
    public class Framerate : MonoBehaviour
    {

        public void SetNewFramerate(int newFramerate)
        {
            Application.targetFrameRate = newFramerate;
        }
    }
}

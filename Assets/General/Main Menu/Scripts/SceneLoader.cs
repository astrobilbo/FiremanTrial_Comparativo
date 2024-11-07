using UnityEngine;
using UnityEngine.SceneManagement;

namespace FiremanTrial.MainMenu
{
    public class SceneLoader : MonoBehaviour
    {
        public void SceneLoad(int scene)
        {
            SceneManager.LoadSceneAsync(scene);
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

namespace FiremanTrial.MainMenu
{
    public class SceneLoader : MonoBehaviour
    {
        public void SceneLoad(string scene)
        {
            SceneManager.LoadSceneAsync(scene);
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

namespace FiremanTrial.General
{
    public class SceneLoader : MonoBehaviour
    {
        public void SceneLoad(int scene)
        {
            SceneManager.LoadSceneAsync(scene);
        }
    }
}
using UnityEngine;

namespace FiremanTrial.WithoutArch
{
    public class Discharge : MonoBehaviour
    {
        [SerializeField] AudioClip clip;
        [SerializeField]AudioSource audioSource;
        public void Play()
        {
            audioSource?.PlayOneShot(clip);
        }
    }
}

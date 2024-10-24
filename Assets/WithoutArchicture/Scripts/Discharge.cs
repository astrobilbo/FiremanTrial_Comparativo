using UnityEngine;

namespace FiremanTrial.WithoutArchitecture
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

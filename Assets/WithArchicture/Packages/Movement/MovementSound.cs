using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiremanTrial.WithArchitecture
{
    public class MovementSound : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip clip;
        [SerializeField] private Movement movement;
        

        private void OnEnable()
        {
            if (movement == null) return;
            
            movement.Moving += PlaySound;
            movement.Stoped += StopSound;
        }
        
            private void OnDisable()
            {
                if (movement == null) return;
                
                movement.Moving -= PlaySound;
                movement.Stoped -= StopSound;
            }


        
        private void PlaySound(Vector3 direction)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
        }

        private void StopSound(Vector3 direction)
        {
            audioSource.Stop();
        }
    }
}

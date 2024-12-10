using FiremanTrial.Movement;
using UnityEngine;

namespace FiremanTrial.Sound
{
    public class MovementSoundFX : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        private IMovingBooleanNotifier _movingBooleanNotifier;
        [SerializeField] private AudioClip clip;
        
        public void Initialize(IMovingBooleanNotifier observer, AudioSource aSource, AudioClip aClip)
        {
            _movingBooleanNotifier = observer;
            audioSource = aSource;
            clip = aClip;
            ChangeClip();
            StopSound();
            SetObserver();
        }

        private void Awake()
        {
            _movingBooleanNotifier = GetComponent(typeof(IMovingBooleanNotifier)) as IMovingBooleanNotifier;
            ChangeClip();
            StopSound();
        }

        private void OnEnable() => SetObserver();
        
        private void OnDisable() => RemoveObserver();

        private void SetObserver()
        {
            if (_movingBooleanNotifier is null) return;
            _movingBooleanNotifier.BooleanObserver += StepSound;
        }
        
        private void RemoveObserver()
        {
            if (_movingBooleanNotifier is null) return;
            _movingBooleanNotifier.BooleanObserver -= StepSound;
        }

        private void StepSound(bool isMoving)
        {
            if (!CanUseAudioSource()) return;

            switch (isMoving)
            {
                case true when !IsPlaying():
                    PlaySound();
                    break;
                case false when IsPlaying():
                    StopSound();
                    break;
            }
        }
        
        private void ChangeClip()
        {
            if (!CanUseAudioSource()) return;
            audioSource.clip = clip;
        }
        
        private bool CanUseAudioSource() => audioSource&& clip;

        private bool IsPlaying() => audioSource.isPlaying;

        private void PlaySound() => audioSource.Play();

        private void StopSound() => audioSource.Stop();
      
    }
}

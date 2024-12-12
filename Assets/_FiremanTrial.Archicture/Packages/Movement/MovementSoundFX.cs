using UnityEngine;

namespace FiremanTrial.Movement
{
    public class MovementSoundFX : MonoBehaviour
    {
        private AudioSource audioSource;
        private IMovingBooleanNotifier _movingBooleanNotifier;
        private AudioClip clip;
        
        public void Initialize(IMovingBooleanNotifier observer, AudioSource aSource, AudioClip aClip)
        {
            _movingBooleanNotifier = observer;
            audioSource = aSource;
            clip = aClip;
            ChangeClip();
            StopSound();
            SetObserver();
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

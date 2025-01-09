using UnityEngine;

namespace FiremanTrial.Movement
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(IMovingBooleanNotifier))]
    public class MovementSoundFX : MonoBehaviour
    {
        private AudioSource _audioSource;
        private IMovingBooleanNotifier _movingBooleanNotifier;
        [SerializeField]private AudioClip clip;
        
        public void Awake()
        {
            _movingBooleanNotifier = GetComponent<MovementHandler>();
            _audioSource = GetComponent<AudioSource>();
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
            _audioSource.clip = clip;
        }
        
        private bool CanUseAudioSource() => _audioSource&& clip;

        private bool IsPlaying() => _audioSource.isPlaying;

        private void PlaySound() => _audioSource.Play();

        private void StopSound() => _audioSource.Stop();
      
    }
}

using System.Collections;
using FiremanTrial.WithArchitecture;
using UnityEngine;
using UnityEngine.Events;

namespace FiremanTrial.Object
{
    [RequireComponent(typeof(InteractiveObject))]
    [RequireComponent(typeof(AudioSource))]
    public class Door : MonoBehaviour
    {
        [SerializeField] private UnityEvent openAction;
        [SerializeField] private UnityEvent closeAction;
        [SerializeField] private Animator animator;
        [SerializeField] private string openDoorTrigger;
        [SerializeField] private string closeDoorTrigger;
        [SerializeField] private bool isOpen;
        [SerializeField] private AudioClip doorSound;
        private bool _isInteracting;
        private bool _isMoving;
        private int _openDoorAnimIndex;
        private int _closeDoorAnimIndex;
        private IEnumerator _doorAnimationCoroutine;
        private InteractiveObject _interactiveObject;
        private AudioSource _audioSource;
        
        private void Awake()
        {
            _openDoorAnimIndex = Animator.StringToHash(openDoorTrigger);
            _closeDoorAnimIndex = Animator.StringToHash(closeDoorTrigger);
            _interactiveObject = GetComponent<InteractiveObject>();
            _audioSource = GetComponent<AudioSource>();
            _audioSource.Stop();
        }

        private void OnEnable() => SetObserver();
        private void OnDisable() => RemoveObserver();

        private void SetObserver()
        {
            if (_interactiveObject is null) return;
            _interactiveObject.InteractAction += IsInteracting;
        }

        private void RemoveObserver()
        {
            if (_interactiveObject is null) return;
            _interactiveObject.InteractAction -= IsInteracting;
        }

        private void IsInteracting(bool isInteracting) => _isInteracting = isInteracting;

        private void TriggerDoorAction(bool opening, int animIndex, UnityEvent actionEvent)
        {
            if (!CanMove(opening)) return;
            Debug.Log("the door is moving", this);
            _audioSource.PlayOneShot(doorSound);
            actionEvent?.Invoke();
            StartCoroutine(_doorAnimationCoroutine = DoorAnimations(animIndex, opening));
        }

        public void TriggerDoorMovement()
        {
            switch (isOpen)
            {
                case true:
                    Debug.Log("the door is closing");
                    Close();
                    break;
                default:
                    Debug.Log("the door is opening");
                    Open();
                    break;
            }
        }
        public bool CanMove(bool direction) => _isInteracting && !_isMoving && isOpen != direction;
        public bool CanMove() => _isInteracting && !_isMoving;

        public void Open() => TriggerDoorAction(true, _openDoorAnimIndex, openAction);

        public void Close() => TriggerDoorAction(false, _closeDoorAnimIndex, closeAction);
        

        IEnumerator DoorAnimations(int animIndex,bool opening)
        {
            _isMoving = true;
            animator.SetTrigger(animIndex);
            
            while (animator.GetCurrentAnimatorStateInfo(0).shortNameHash != animIndex)
            {
                yield return null;
            }
            
            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime<1)
            {
                yield return null;
            }
            
            isOpen = opening;
            EndMoving();
        }

        private void EndMoving()
        {
            _isMoving = false;
            if (_doorAnimationCoroutine == null) return;
            
            StopCoroutine(_doorAnimationCoroutine);
            _doorAnimationCoroutine = null;
        }
    }
}
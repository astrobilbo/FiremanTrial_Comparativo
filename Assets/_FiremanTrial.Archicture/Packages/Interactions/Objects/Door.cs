using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace FiremanTrial.WithArchitecture
{
    [RequireComponent(typeof(InteractiveObject))]
    public class Door : MonoBehaviour
    {
        [SerializeField] private UnityEvent openAction;
        [SerializeField] private UnityEvent closeAction;
        [SerializeField] private Animator animator;
        [SerializeField] private string openDoorTrigger;
        [SerializeField] private string closeDoorTrigger;
        [SerializeField] private bool isOpen;
        private bool _isInteracting;
        private bool _isMoving;
        private int _openDoorAnimIndex;
        private int _closeDoorAnimIndex;
        private IEnumerator _doorAnimationCoroutine;
        private InteractiveObject _interactiveObject;
        
        private void Awake()
        {
            _openDoorAnimIndex = Animator.StringToHash(openDoorTrigger);
            _closeDoorAnimIndex = Animator.StringToHash(closeDoorTrigger);
            _interactiveObject = GetComponent<InteractiveObject>();
        }
        

        private void TriggerDoorAction(bool opening, int animIndex, UnityEvent actionEvent)
        {
            if (CanMove(opening)) return;

            actionEvent?.Invoke();
            StartCoroutine(_doorAnimationCoroutine = DoorAnimations(animIndex, opening));
        }

        public bool CanMove(bool opening)
        {
            return !_isInteracting || _isMoving || isOpen == opening;
        }

        public void Open() => TriggerDoorAction(true, _openDoorAnimIndex, openAction);

        public void Close() => TriggerDoorAction(false, _closeDoorAnimIndex, closeAction);
        

        IEnumerator DoorAnimations(int animIndex,bool opening)
        {
            _isMoving = true;
            animator.SetTrigger(animIndex);
            var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(stateInfo.normalizedTime);
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
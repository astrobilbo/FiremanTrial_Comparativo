using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace FiremanTrial.WithArchitecture
{
    public class Door : InteractiveObject
    {
        [SerializeField] private UnityEvent openAction;
        [SerializeField] private UnityEvent closeAction;
        [SerializeField] private Animator animator;
        [SerializeField] private string openDoorAnimationName;
        [SerializeField] private string closeDoorAnimationName;
        [SerializeField] private bool isOpen;
        private bool _isMoving;
        private int _openDoorAnimIndex;
        private int _closeDoorAnimIndex;
        private IEnumerator _doorAnimationCoroutine;

        private void Start()
        {
            _openDoorAnimIndex = Animator.StringToHash(openDoorAnimationName);
            _closeDoorAnimIndex = Animator.StringToHash(closeDoorAnimationName);
            
            ValidateAnimationHash(_openDoorAnimIndex, openDoorAnimationName);
            ValidateAnimationHash(_closeDoorAnimIndex, closeDoorAnimationName);
        }

        private void ValidateAnimationHash(int animHash, string animationName)
        {
            if (!IsAnimationValid(animHash))
            {
                Debug.LogWarning($"Animation '{animationName}' not found on Animator attached to '{gameObject.name}'.", this);
            }
        }
        private bool IsAnimationValid(int animHash)
        {
            var animatorController = animator.runtimeAnimatorController;
            if (animatorController == null) return false;

            foreach (var clip in animatorController.animationClips)
            {
                if (Animator.StringToHash(clip.name) == animHash)
                    return true;
            }
            return false;
        }

        private void TriggerDoorAction(bool opening, int animIndex, UnityEvent actionEvent)
        {
            if (!IsInteracting || _isMoving || isOpen == opening) return;

            actionEvent?.Invoke();
            StartCoroutine(_doorAnimationCoroutine = DoorAnimations(animIndex, opening));
        }

        public void Open() => TriggerDoorAction(true, _openDoorAnimIndex, openAction);

        public void Close() => TriggerDoorAction(false, _closeDoorAnimIndex, closeAction);
        

        IEnumerator DoorAnimations(int animIndex,bool opening)
        {
            _isMoving = true;
            InteractAction?.Invoke(false);
            animator.Play(animIndex);
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
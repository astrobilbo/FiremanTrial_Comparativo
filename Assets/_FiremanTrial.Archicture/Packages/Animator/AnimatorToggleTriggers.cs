using System;
using System.Collections.Generic;
using UnityEngine;

namespace FiremanTrial.WithArchitecture.Animator
{
    public class AnimatorToggleTriggers : MonoBehaviour
    {
        [SerializeField] private AnimatorTrigger animatorTriggerOn;
        [SerializeField] private AnimatorTrigger animatorTriggerOff;
        private AnimatorTrigger _animatorTriggerActive;

        private void Awake()
        {
            _animatorTriggerActive = animatorTriggerOff;
        }

        public void Toggle()
        {
            _animatorTriggerActive = _animatorTriggerActive == animatorTriggerOn ? animatorTriggerOff : animatorTriggerOn;
            _animatorTriggerActive.Activate();
        }
    }
}
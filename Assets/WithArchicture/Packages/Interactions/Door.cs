using System;
using UnityEngine;
using UnityEngine.Events;

namespace FiremanTrial.WithArchitecture
{
    public class Door : InteractiveObject
    {
        [SerializeField] private UnityEvent openCloseAction;
        private bool _opening;

        public bool CanOpen() => !_opening && !IsInteracting;

        public void OpenClose()
        {
            openCloseAction.Invoke();
            _opening = true;
            InteractAction?.Invoke(false);
            Debug.Log("Opening Door",this);
        }

        public void EndOpening()
        {
            Debug.Log("End Opening Door",this);
            _opening = false;
            EndInteraction();
        }
    }
}
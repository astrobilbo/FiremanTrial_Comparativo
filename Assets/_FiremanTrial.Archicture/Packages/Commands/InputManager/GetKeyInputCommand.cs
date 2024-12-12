using System;
using FiremanTrial.Commands;
using UnityEngine;
using UnityEngine.Events;

namespace FiremanTrial.WithArchitecture.Inputs
{
    public class GetKeyInputCommand : MonoBehaviour
    {
        [SerializeField] private KeyCode keyCode;
        [SerializeField] private  Command onKeyDownCommand;
        [SerializeField] private  Command onKeyCommand;
        [SerializeField] private  Command onKeyUpCommand;
        [SerializeField] private UnityEvent onKeyDownEvent;
        [SerializeField] private UnityEvent onKeyEvent;
        [SerializeField] private UnityEvent onKeyUpEvent;
      
        private void Update() => InputHandler();
        
        private void InputHandler()
        {
            KeyDownCommand();
            KeyPressedCommand();
            KeyUpCommand();
        }

        private void KeyDownCommand()
        {
            if (!Input.GetKeyDown(keyCode)) return;
            onKeyDownCommand?.Execute();
            onKeyDownEvent?.Invoke();
        }

        private void KeyPressedCommand()
        {
            if (!Input.GetKey(keyCode)) return;
            onKeyCommand?.Execute();
            onKeyEvent?.Invoke();
        }

        private void KeyUpCommand()
        {
            if (!Input.GetKeyUp(keyCode)) return;
            onKeyUpCommand?.Execute();
            onKeyUpEvent?.Invoke();
        }
    }
}
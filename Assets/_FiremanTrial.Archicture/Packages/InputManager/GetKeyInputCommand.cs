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

        [SerializeField] private bool useCooldown = true; 
        [SerializeField] private float inputCooldown = 0.2f; 

        private float lastInputTime = 0f; 
        
        private void Update() => InputHandler();
        
        private void InputHandler()
        {
            if (useCooldown && !CanProcessInput()) return;
            KeyDownCommand();
            KeyPressedCommand();
            KeyUpCommand();
        }
        
        private bool CanProcessInput()
        {
            return Time.time - lastInputTime >= inputCooldown;
        }

        private void ProcessInput()
        {
            lastInputTime = Time.time;
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
            ProcessInput();
        }
    }
}
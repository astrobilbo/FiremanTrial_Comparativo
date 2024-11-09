using System;
using FiremanTrial.WithArchitecture.Commands;
using UnityEngine;

namespace FiremanTrial.WithArchitecture.Commands
{
    public abstract class Command: MonoBehaviour
    {
        public Action ActionExecuted;
        [SerializeField] protected KeyCode keyCode;
        [SerializeField] protected string axisInput;
        private const float Threshold = 0.01f;
        private float _lastAxisValue = 0f;
        protected virtual void Awake()
        {
            CommandLogger.RegisterCommand(this);
        }

        protected void HandleAxisInput()
        {
            float axisValue = Input.GetAxis(axisInput);

            // Only execute if axis value changes significantly to avoid noise
            if (Mathf.Abs(axisValue) > Threshold && Mathf.Abs(axisValue - _lastAxisValue) > Threshold)
            {
                Execute(axisValue);
                _lastAxisValue = axisValue;
            }
            else if (Mathf.Abs(axisValue) <= Threshold)
            {
                _lastAxisValue = 0f;  // Reset if below threshold
            }
        }
        
        public virtual string CommandID { get; }

        protected virtual void Execute()
        {
            CommandLogger.LogCommand(this);
            ActionExecuted?.Invoke();
        }
        protected virtual void Execute(float value) {}
        public string Log(float time)=>$"commandID: {CommandID} executed at {time}";
        public string Log(float time,float executionValue)=>$"commandID: {CommandID} with value {executionValue} executed at {time:00:00}";

    }
}

using System;
using FiremanTrial.Commands;
using UnityEngine;

namespace FiremanTrial.Commands
{
    public abstract class Command: MonoBehaviour
    {
        public Action ActionExecuted;
        protected virtual void Awake()
        {
            CommandLogger.RegisterCommand(this);
        }
        
        public virtual string CommandID { get; }

        public virtual void Execute()
        {
            CommandLogger.LogCommand(this);
            ActionExecuted?.Invoke();
        }

        public virtual void Execute(float value) { }
        public string Log(float time)=>$"commandID: {CommandID} executed at {time}";
        public string Log(float time,float executionValue)=>$"commandID: {CommandID} with value {executionValue} executed at {time:00:00}";

    }
}

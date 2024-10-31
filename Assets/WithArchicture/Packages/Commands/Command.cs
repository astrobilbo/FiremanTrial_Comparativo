using FiremanTrial.WithArchitecture.Commands;
using UnityEngine;

namespace FiremanTrial.WithArchitecture.Commands
{
    public abstract class Command: MonoBehaviour
    {
        protected virtual void Awake()
        {
            CommandLogger.RegisterCommand(this);
        }
        
        public virtual string CommandID { get; }
        public virtual void Execute(){}
        public virtual void Execute(float value) {}
        public string Log(float time)=>$"commandID: {CommandID} executed at {time}";
        public string Log(float time,float executionValue)=>$"commandID: {CommandID} with value {executionValue} executed at {time:00:00}";

    }
}

using System;
using FiremanTrial.WithArchitecture.Commands;
using UnityEngine;
using UnityEngine.Events;

namespace FiremanTrial.WithArchitecture.Quest
{
  
    public class QuestStep : MonoBehaviour
    {
        public string description="";
        public UnityEvent initiate;
        public bool activeStep=false;
        public  bool IsCompleted { get; protected set; }
        public event Action OnCompleted;
        public Command commandToFollow;
        public SphereOverlapInteractions positionToGo;

        private void OnEnable()
        {
            if (commandToFollow != null) commandToFollow.ActionExecuted += CompleteStep;
            if (positionToGo != null) positionToGo.ObjectInPositionRange += CompleteStep;

        }

        private void OnDisable()
        {
            if (commandToFollow != null) commandToFollow.ActionExecuted -= CompleteStep;
            if (positionToGo != null) positionToGo.ObjectInPositionRange -= CompleteStep;

        }

        public void InitiateStep()
        {
            initiate?.Invoke();
        }

        private void CompleteStep()
        {
            
            IsCompleted = true;
            OnCompleted?.Invoke();
        }
    }
}

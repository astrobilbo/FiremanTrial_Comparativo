using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace FiremanTrial.Quest
{
    [CreateAssetMenu(menuName = "Quests/QuestStep")]
    public class QuestStep : ScriptableObject
    {
        public event Action OnStart;        
        public event Action OnCompleted;
        public event Action OnFail;
        public string stepObjective="";
        public bool activeStep=false;
        public bool isCompleted=false;

        public void InitiateStep()
        {
            activeStep = true;
            OnStart?.Invoke();
        }

        public void CompleteStep()
        {
            if (!activeStep) return;
            
            activeStep = false;
            isCompleted = true;
            OnCompleted?.Invoke();
        }

        public void StepFailed()
        {
            OnFail?.Invoke();
        }
    }
}

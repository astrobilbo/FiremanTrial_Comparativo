using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiremanTrial.Quest
{
    public class Quest : MonoBehaviour
    {
        public event  Action<bool> ActiveQuest;
        public event  Action<string> QuestDescription;
        [SerializeField] private List<Quest> requirements ;
        [SerializeField] private List<QuestStep> steps ;
        private int _currentStep=0;

        public void StartQuest()
        {
            if (!CanStartTheQuest()) return;
            ActiveQuest?.Invoke(true);
            QuestStepSetting();
        }

        private bool CanStartTheQuest()
        {
            if (steps != null && steps.Count != 0 && IsAvailable()) return true;
            Debug.LogWarning("No steps available in the quest.");
            return false;
        }

        private void NextStep()
        {
            steps[_currentStep].OnCompleted-= NextStep;
            steps[_currentStep].activeStep = true;
            if (IsLastStep()) return;
            _currentStep++;
            QuestStepSetting();
        }

        private void QuestStepSetting()
        {
            var activeStep=steps[_currentStep];
            activeStep.OnCompleted += NextStep;
            activeStep.activeStep = true;
            QuestDescription?.Invoke(activeStep.description);
            Debug.Log(activeStep.description);
        }

        private bool IsLastStep()
        {
            if (IsCompleted)
            {
                EndQuest();
            }
            return IsCompleted;
        }

        private bool IsAvailable() => requirements == null || requirements.TrueForAll(req => req.IsCompleted);

        private void EndQuest() => ActiveQuest?.Invoke(false);

        public bool IsCompleted => steps.TrueForAll(step => step.IsCompleted);
    }
}

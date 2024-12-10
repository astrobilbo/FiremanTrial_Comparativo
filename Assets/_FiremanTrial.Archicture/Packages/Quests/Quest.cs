using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiremanTrial.Quest
{
    public class Quest : MonoBehaviour
    {
        public  Action<Quest> ActiveQuest;
        public  Action DesactiveQuest;
        public  Action<string> QuestDescription;
        [SerializeField] private GameObject questInitializer;
        [SerializeField] private List<Quest> requirements ;
        [SerializeField] private List<QuestStep> steps ;
        [SerializeField] private string questName;
        [SerializeField, TextArea(0,5000)] private string questDescription;
        
        private int _currentStep=0;

        private void Awake()
        {
            QuestManager.AddQuest(this);
            questInitializer.SetActive(false);
            ShowQuestIfAvailable();
        }

        private void OnEnable()
        {
            QuestManager.EndQuest += ShowQuestIfAvailable;
        }

        private void OnDisable()
        {
            QuestManager.EndQuest -= ShowQuestIfAvailable;
        }


        private void ShowQuestIfAvailable()
        {
            if (IsAvailable())
            {
                questInitializer.SetActive(true);
            }
        }
        
        public void StartQuest()
        {
            if (!CanStartTheQuest()) return;
            ActiveQuest?.Invoke(this);
            questInitializer.SetActive(false);
            foreach (var step in steps)
            {
                step.isCompleted = false;
                step.activeStep = false;
            }
            QuestStepSetting();
        }

        private bool CanStartTheQuest()
        {
            var canStart = false;
            if (!QuestManager.HaveActiveQuest()) canStart = true;
            else if (steps?.Count != 0) canStart = true;
            else if (IsAvailable()) canStart = true;
            return canStart;
        }

        private void NextStep()
        {
            steps[_currentStep].OnCompleted-= NextStep;
            if (IsLastStep()) return;
            _currentStep++;
            QuestStepSetting();
        }

        private void QuestStepSetting()
        {
            var activeStep=steps[_currentStep];
            activeStep.OnCompleted += NextStep;
            activeStep.InitiateStep();
            QuestDescription?.Invoke(activeStep.stepObjective);
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

        private void EndQuest() => DesactiveQuest?.Invoke();

        private bool IsCompleted => steps.TrueForAll(step => step.isCompleted);
        
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using FiremanTrial.WithArchitecture;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace FiremanTrial.Quest
{
    [RequireComponent(typeof(AudioSource))]
    public class Quest : MonoBehaviour
    {
        public  Action<Quest> Active;
        public  Action Desactive;
        public  Action<string> QuestObjective;
        public  Action<string> QuestDescription;        
        public  Action<string> QuestInformation;
        
        [Header("Quest external properties")]
        [SerializeField] private Fire fire;
        [SerializeField] private GameObject questInitializer;
        [SerializeField] private List<Quest> requirements ;
        [SerializeField] private List<QuestStep> steps ;
        [SerializeField] private string questName;

        [Space(2)] // 2 espaços para facilitar a leitura no inspetor

        [Header("External actions")]
        [SerializeField] private UnityActions actions;

        [Serializable] internal class UnityActions
        {
            public UnityEvent started;
            public UnityEvent failed;
            public UnityEvent win;
        }
        
        [Space (2)] // 2 espaços para facilitar a leitura no inspetor
        
        [Header("Quest feedback")]
        [SerializeField, TextArea(0,5000)] private string questDescription;
        [SerializeField, TextArea(0,5000)] private string questInformation;
        [SerializeField] private Sounds questSounds;
        
        [Serializable] internal class Sounds
        {
            public AudioClip winClip; 
            public  AudioClip loseClip;
            public AudioClip newStepClip;
        }

        private int _currentStep=0;
        private AudioSource _audioSource;
        
        private void Awake()
        {
            QuestManager.AddQuest(this);
            questInitializer.SetActive(false);
            ShowQuestIfAvailable();
            foreach (var step in steps)
            {
                step.isCompleted = false;
                step.activeStep = false;
            }
            _audioSource=GetComponent<AudioSource>();
            _audioSource.Stop();
        }

        private void OnEnable() => QuestManager.EndQuest += ShowQuestIfAvailable;

        private void OnDisable() => QuestManager.EndQuest -= ShowQuestIfAvailable;
        
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
            Debug.Log($"Quest: {questName} Started");
            Active?.Invoke(this);
            actions.started?.Invoke();
            questInitializer.SetActive(false);
            foreach (var step in steps)
            {
                step.isCompleted = false;
                step.activeStep = false;
            }
            QuestDescription?.Invoke(questDescription);
            QuestInformation?.Invoke(questInformation);
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
            steps[_currentStep].OnFail-= LoseQuest;
            if (IsLastStep()) return;
            _currentStep++;
            QuestStepSetting();
            PlayOneShotSound(questSounds.newStepClip);
        }

        private void QuestStepSetting()
        {
            var activeStep=steps[_currentStep];
            activeStep.OnCompleted += NextStep;
            activeStep.OnFail += LoseQuest;
            activeStep.InitiateStep();
            QuestObjective?.Invoke(activeStep.stepObjective);
        }

        private bool IsLastStep()
        {
            if (IsCompleted)
            {
                WinQuest();
            }
            return IsCompleted;
        }

        private bool IsAvailable() => requirements == null || requirements.TrueForAll(req => req.IsCompleted);

        private void WinQuest()
        {
            Desactive?.Invoke();
            PlayOneShotSound(questSounds.winClip);
            actions.win?.Invoke();
        }

        private bool IsCompleted => steps.TrueForAll(step => step.isCompleted);

        public bool Completed() => IsCompleted;

        private void LoseQuest()
        {
            PlayOneShotSound(questSounds.loseClip);
            questInitializer.SetActive(true);
            actions.failed?.Invoke();
        }

        private void PlayOneShotSound(AudioClip sound)
        {
            if (_audioSource is null) return;
            _audioSource.PlayOneShot(sound);
        }
    }
}

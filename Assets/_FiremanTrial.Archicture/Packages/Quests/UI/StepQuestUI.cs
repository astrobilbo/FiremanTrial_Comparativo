using System;
using System.Collections;
using System.Collections.Generic;
using FiremanTrial.General;
using TMPro;
using UnityEngine;

namespace FiremanTrial.Quest.UI
{
    public class StepQuestUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI stepText;
        [SerializeField] private CanvasGroup stepQuestPivot;
        private Quest _activeQuest;

        private void Awake()
        {
            CanvasGroupManager.Visible(false,stepQuestPivot);
        }

        private void OnEnable()
        {
            QuestManager.StartQuest += GetActiveQuest;
            QuestManager.EndQuest += EndActiveQuest;
        }

        private void OnDisable()
        {
            QuestManager.StartQuest -= GetActiveQuest;
            QuestManager.EndQuest -= EndActiveQuest;
            
        }

        private void GetActiveQuest()
        {
            CanvasGroupManager.Visible(true,stepQuestPivot);
            _activeQuest = QuestManager.GetActiveQuest();
            _activeQuest.QuestDescription += UpdateTextByStep;
        }

        

        private void EndActiveQuest()
        {
            CanvasGroupManager.Visible(false,stepQuestPivot);
            UpdateTextByStep();
            _activeQuest.QuestDescription -= UpdateTextByStep;
            _activeQuest = null;
        }

        private void UpdateTextByStep(string description = "")
        {
            stepText.text = description;
        }

    }
}

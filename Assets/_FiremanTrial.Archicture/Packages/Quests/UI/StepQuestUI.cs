using System;
using TMPro;
using UnityEngine;

namespace FiremanTrial.Quest.UI
{
    public class StepQuestUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI objectiveText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI infoText;

        private Quest _activeQuest;

        private void Awake()
        {
            UpdateObjectiveText();
            UpdateDescriptionText();
            UpdateInfoText();
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
            _activeQuest = QuestManager.GetActiveQuest();
            _activeQuest.QuestObjective += UpdateObjectiveText;
            _activeQuest.QuestDescription += UpdateDescriptionText;
            _activeQuest.QuestInformation += UpdateInfoText;

        }
        
        private void EndActiveQuest()
        {
            UpdateObjectiveText();
            UpdateDescriptionText();
            UpdateInfoText();
            _activeQuest.QuestObjective -= UpdateObjectiveText;
            _activeQuest.QuestDescription -= UpdateDescriptionText;
            _activeQuest.QuestInformation -= UpdateInfoText;
            _activeQuest = null;
        }

        private void UpdateObjectiveText(string text = "Nenhuma missão iniciada.")
        {
            if (objectiveText == null) return;
            objectiveText.text = text;
        }
        
        private void UpdateDescriptionText(string text = "Nenhuma missão iniciada.")
        {
            if (descriptionText == null) return;
            descriptionText.text = text;
        } 
        
        private void UpdateInfoText(string text = "Nenhuma missão iniciada.")
        {
            if (infoText == null) return;
            infoText.text = text;
        }
    }
}

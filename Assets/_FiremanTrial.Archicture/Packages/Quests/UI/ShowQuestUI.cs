using System;
using System.Collections;
using System.Collections.Generic;
using FiremanTrial.General;
using UnityEngine;

namespace FiremanTrial.Quest.UI
{
    public class ShowQuestUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup infoCG;
        [SerializeField] private CanvasGroup questCG;
        
        private void OnEnable() => QuestManager.EndQuest += EndActiveQuest;

        private void OnDisable() => QuestManager.EndQuest -= EndActiveQuest;

        public void ShowInfo()
        {
            switch (infoCG.blocksRaycasts)
            {
                case false:
                    CanvasGroupManager.Visible(Active(), infoCG);
                    CanvasGroupManager.Visible(false, questCG);
                    break;
                default:
                    CanvasGroupManager.Visible(false, infoCG);
                    break;
            }
        }

        public void ShowQuest()
        {
            switch (questCG.blocksRaycasts)
            {
                case false:
                    CanvasGroupManager.Visible(Active(), questCG);
                    CanvasGroupManager.Visible(false, infoCG);
                    break;
                default:
                    CanvasGroupManager.Visible(false, questCG);
                    break;
            }
        }
       

        private void EndActiveQuest()
        {
            CanvasGroupManager.Visible(false, questCG);
            CanvasGroupManager.Visible(false, infoCG);
        }
        
        private static bool Active()
        {
            return QuestManager.GetActiveQuest() is not null;
        }
    }
}

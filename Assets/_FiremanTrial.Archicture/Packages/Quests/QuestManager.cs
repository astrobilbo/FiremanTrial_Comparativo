using System;
using System.Collections.Generic;
using FiremanTrial.Manager;

namespace FiremanTrial.Quest
{
    public static class QuestManager
    {
        public static Action StartQuest;
        public static Action EndQuest;
        
        private static List<Quest> _quests = new List<Quest>();
        private static Quest _activeQuest;

        public static void AddQuest(Quest quest)
        {
            _quests.Add(quest);
            quest.Active += SetActiveQuest;
            quest.Desactive += EndActiveQuest;
        }

        public static void RemoveQuest(Quest quest)
        {
            quest.Active -= SetActiveQuest;
            quest.Desactive -= EndActiveQuest;
            _quests.Remove(quest);
        }

        private static bool CheckVictory()
        {
            foreach (var quest in _quests)
            {
                if (!quest.Completed())
                {
                    return false;
                }
            }

            return true;
        }
        public static bool HaveActiveQuest()
        {
            return _activeQuest != null;
        }

        private static void SetActiveQuest(Quest quest)
        {
            _activeQuest = quest;
            StartQuest?.Invoke();
        }

        private static void EndActiveQuest()
        {
            _activeQuest = null;
            EndQuest?.Invoke();
            if (CheckVictory())
            {
                GameManager.SetGameState(GameState.Win);
            }
        }

        public static Quest GetActiveQuest()
        {
            return _activeQuest;
        }
       
    }
}

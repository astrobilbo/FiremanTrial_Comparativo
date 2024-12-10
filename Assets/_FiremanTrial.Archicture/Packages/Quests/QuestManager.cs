using System;
using System.Collections.Generic;

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
            quest.ActiveQuest += SetActiveQuest;
            quest.DesactiveQuest += EndActiveQuest;
        }
        
        public static bool HaveActiveQuest()
        {
            return _activeQuest != null;
        }
        
        private static void OnDisable()
        {
            foreach (var quest in _quests)
            {
                quest.ActiveQuest -= SetActiveQuest;
                quest.DesactiveQuest -= EndActiveQuest;
            }
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
        }

        public static Quest GetActiveQuest()
        {
            return _activeQuest;
        }
       
    }
}

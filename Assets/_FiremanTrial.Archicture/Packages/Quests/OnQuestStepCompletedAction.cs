using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace FiremanTrial.Quest
{
    public class OnQuestStepCompletedAction : MonoBehaviour
    {
        [SerializeField] private QuestStep step;
        [SerializeField] private UnityEvent onCompleted;

        private void OnEnable()
        {
            if (step == null) return;
            
            step.OnCompleted += OnCompletedInvoke;
        }

        private void OnDisable()
        {
            if (step == null) return; 
            
            step.OnCompleted -= OnCompletedInvoke;
        }

        private void OnCompletedInvoke()
        {
            onCompleted?.Invoke();
        }
        
    }
}
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace FiremanTrial.Quest
{
    public class OnQuestStepFailAction : MonoBehaviour
    {
        [SerializeField] private QuestStep step;
        [SerializeField] private UnityEvent onFail;

        private void OnEnable()
        {
            if (step == null) return;
            
            step.OnFail += OnFailInvoke;
        }

        private void OnDisable()
        {
            if (step == null) return; 
            
            step.OnFail -= OnFailInvoke;
        }
        
        private void OnFailInvoke()
        {
            onFail?.Invoke();
        }
    }
}
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace FiremanTrial.Quest
{
    public class OnQuestStepStartAction : MonoBehaviour
    {
        [SerializeField] private QuestStep step;
        [SerializeField] private UnityEvent onStart;

        private void OnEnable()
        {
            if (step == null) return;
            
            step.OnStart += OnStartInvoke;
        }

        private void OnDisable()
        {
            if (step == null) return; 
            
            step.OnStart -= OnStartInvoke;
        }

        private void OnStartInvoke()
        {
            onStart?.Invoke();
        }

       
    }
}
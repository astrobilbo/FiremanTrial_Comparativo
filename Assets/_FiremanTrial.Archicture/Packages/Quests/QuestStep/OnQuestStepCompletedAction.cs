using UnityEngine;
using UnityEngine.Events;

namespace FiremanTrial.Quest
{
    public class OnQuestStepCompletedAction : MonoBehaviour
    {
        [SerializeField] private QuestStep step;
        [SerializeField] private UnityEvent onCompleted;

        private void OnEnable() => step.OnCompleted += OnCompletedInvoke;

        private void OnDisable() => step.OnCompleted -= OnCompletedInvoke;

        private void OnCompletedInvoke() => onCompleted?.Invoke();
    }
}
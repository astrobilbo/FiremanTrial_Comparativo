using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace FiremanTrial.Quest
{
    public class OnQuestStepStartAction : MonoBehaviour
    {
        [SerializeField] private QuestStep step;
        [SerializeField] private UnityEvent onStart;

        private void OnEnable() => step.OnStart += OnStartInvoke;

        private void OnDisable() => step.OnStart -= OnStartInvoke;

        private void OnStartInvoke() => onStart?.Invoke();
    }
}
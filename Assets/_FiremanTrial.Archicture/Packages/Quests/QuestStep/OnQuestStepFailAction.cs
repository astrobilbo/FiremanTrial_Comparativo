using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace FiremanTrial.Quest
{
    public class OnQuestStepFailAction : MonoBehaviour
    {
        [SerializeField] private QuestStep step;
        [SerializeField] private UnityEvent onFail;

        private void OnEnable() => step.OnFail += OnFailInvoke;

        private void OnDisable() => step.OnFail -= OnFailInvoke;

        private void OnFailInvoke() => onFail?.Invoke();
    }
}
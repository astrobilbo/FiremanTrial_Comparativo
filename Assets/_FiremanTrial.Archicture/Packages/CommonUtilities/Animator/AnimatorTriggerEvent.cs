using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace FiremanTrial.WithArchitecture.Animator
{
    public class AnimatorTriggerEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent action;
        public void Execute()
        {
            action?.Invoke();
        }

    }
}
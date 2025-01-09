using UnityEngine;
using UnityEngine.Events;

namespace FiremanTrial.WithArchitecture.Inputs
{
    public class InvokeActions : MonoBehaviour
    {
        [SerializeField] private UnityEvent actions;

        public void Invoke(float timeToInvoke)
        {
            Invoke(nameof(Action), timeToInvoke);
        }

        private void Action()
        {
            actions?.Invoke();
        }
    }
}
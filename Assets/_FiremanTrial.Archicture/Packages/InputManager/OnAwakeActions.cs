using UnityEngine;
using UnityEngine.Events;

namespace FiremanTrial.WithArchitecture.Inputs
{
    public class OnAwakeActions : MonoBehaviour
    {
        [SerializeField] private UnityEvent actions;
        private void Awake() => actions?.Invoke();
    }
}
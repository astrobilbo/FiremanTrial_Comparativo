using UnityEngine;
using UnityEngine.Events;

namespace FiremanTrial.WithArchitecture.Inputs
{
    public class OnStartActions : MonoBehaviour
    {
        [SerializeField] private UnityEvent actions;
        private void Start() => actions?.Invoke();
    }
}
using FiremanTrial.WithArchitecture.Commands;
using UnityEngine;

namespace FiremanTrial
{
    public class GetAxesInputCommand : MonoBehaviour
    {
        [SerializeField] private string axesInput;
        [SerializeField] private Command command;
        private const float Threshold = 0.01f;
        private float lastAxisInput = 0f;

        void Update()
        {
            float axisInput = Input.GetAxis(axesInput);

            if (Mathf.Abs(axisInput) > Threshold && 
                Mathf.Abs(axisInput - lastAxisInput) > Threshold)
            {
                command.Execute(axisInput);
                lastAxisInput = axisInput; 
            }
            else if (Mathf.Abs(axisInput) <= Threshold)
            {
                lastAxisInput = 0f; 
            }
        }
    }
}

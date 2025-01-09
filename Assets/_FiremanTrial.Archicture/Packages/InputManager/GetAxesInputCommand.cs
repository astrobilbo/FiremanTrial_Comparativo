using FiremanTrial.Commands;
using UnityEngine;

namespace FiremanTrial.WithArchitecture.Inputs
{
    public class GetAxesInputCommand : MonoBehaviour
    {
        [SerializeField] private Command command;
        [SerializeField] protected string axisInput;
        private const float Threshold = 0.01f;
        private float _lastAxisValue = 0f;

        private void Update()
        {
            if (!string.IsNullOrEmpty(axisInput)) HandleAxisInput();
        }

        private void HandleAxisInput()
        {
            var axisValue = Input.GetAxis(axisInput);

            switch (Mathf.Abs(axisValue))
            {
                case > Threshold when Mathf.Abs(axisValue - _lastAxisValue) > Threshold:
                    command.Execute(axisValue);
                    _lastAxisValue = axisValue;
                    break;
                case <= Threshold:
                    _lastAxisValue = 0f;
                    break;
            }
        }
    }
}
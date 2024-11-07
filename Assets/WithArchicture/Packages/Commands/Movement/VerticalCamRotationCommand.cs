using UnityEngine;

namespace FiremanTrial.WithArchitecture.Commands
{
    public class VerticalCamRotationCommand : Command
    {
        [SerializeField] private CameraController movement;
        public override string CommandID => nameof(VerticalCamRotationCommand);
        private void Update()
        {
            if (!string.IsNullOrEmpty(axisInput)) HandleAxisInput();
        }

        protected override void Execute(float value)
        {
            movement.VerticalRotation(value);
        }
    }
}
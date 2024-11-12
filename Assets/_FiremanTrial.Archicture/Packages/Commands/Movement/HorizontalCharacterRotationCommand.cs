using UnityEngine;

namespace FiremanTrial.WithArchitecture.Commands
{
    public class HorizontalCharacterRotationCommand : Command
    {
        [SerializeField] private Movement movement;

        public override string CommandID => nameof(HorizontalCharacterRotationCommand);
        private void Update()
        {
            if (!string.IsNullOrEmpty(axisInput)) HandleAxisInput();
        }

        protected override void Execute(float value)
        {
            movement.HorizontalCharacterRotation(value);
        }
    }
}
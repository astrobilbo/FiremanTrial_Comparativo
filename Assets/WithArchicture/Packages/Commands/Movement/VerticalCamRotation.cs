using UnityEngine;

namespace FiremanTrial.WithArchitecture.Commands
{
    public class VerticalCamRotation : Command
    {
        [SerializeField] private Movement movement;
        public override string CommandID => nameof(VerticalCamRotation);

        public override void Execute(float value)
        {
            if (!movement.Active) return;
            
            movement.VerticalCamRotation(value);
        }
    }
}
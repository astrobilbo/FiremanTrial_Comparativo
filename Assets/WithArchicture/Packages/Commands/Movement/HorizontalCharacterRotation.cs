using UnityEngine;

namespace FiremanTrial.WithArchitecture.Commands
{
    public class HorizontalCharacterRotation : Command
    {
        [SerializeField] private Movement movement;
        public override string CommandID => nameof(HorizontalCharacterRotation);

        public override void Execute(float value)
        {
            if (!movement.Active) return;
            
            movement.HorizontalCharacterRotation(value);
        }
    }
}
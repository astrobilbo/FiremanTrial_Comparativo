using FiremanTrial.Movement;
using UnityEngine;

namespace FiremanTrial.Commands
{
    public class HorizontalCharacterRotationCommand : Command
    {
        [SerializeField] private MovementHandler movement;
        public override string CommandID => nameof(HorizontalCharacterRotationCommand);
        
        public override void Execute(float value) => movement.HandleRotationInput(value);
    }
}
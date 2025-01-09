using FiremanTrial.Movement;
using UnityEngine;

namespace FiremanTrial.Commands
{
    public class StopMoveCommand : Command
    {
        [SerializeField] private MovementHandler movement;
        [SerializeField] private MovementDirection direction;
        public override string CommandID =>  nameof(StopMoveCommand)+"_"+direction;

        public override void Execute() => movement.RemoveMovementInput(direction);
    }
}
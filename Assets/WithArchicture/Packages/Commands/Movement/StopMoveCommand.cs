using UnityEngine;

namespace FiremanTrial.WithArchitecture.Commands
{
    public class StopMoveCommand : Command
    {
        [SerializeField] private Movement movement;
        [SerializeField] private MovementDirection direction;
        public override string CommandID =>  nameof(MoveCommand)+"_"+direction;

        public override void Execute()
        {
            if (!movement.Active) return;

            movement.StopMove(direction);
        }
    }
}
using UnityEngine;

namespace FiremanTrial.WithArchitecture.Commands
{
    public class StopMoveCommand : Command
    {
        [SerializeField] private Movement movement;
        [SerializeField] private MovementDirection direction;
        public override string CommandID =>  nameof(MoveCommand)+"_"+direction;
        private void Update()
        {
            if (keyCode != KeyCode.None && Input.GetKeyUp(keyCode)) Execute();
        }

        protected override void Execute()
        {
            movement.StopMove(direction);
        }
    }
}
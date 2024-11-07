using UnityEngine;

namespace FiremanTrial.WithArchitecture.Commands
{ 
    public class MoveCommand : Command
    {
        [SerializeField] private Movement movement;
        [SerializeField] private MovementDirection direction;
        public override string CommandID => nameof(MoveCommand)+"_"+direction;
        private void Update()
        {
            if (keyCode != KeyCode.None && Input.GetKeyDown(keyCode)) Execute();
        }

        protected override void Execute()
        {
            movement.StartMove(direction);
        }
    }
}
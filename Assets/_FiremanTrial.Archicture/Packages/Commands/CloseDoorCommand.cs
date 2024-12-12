using FiremanTrial.Commands;
using FiremanTrial.WithArchitecture;
using UnityEngine;

namespace FiremanTrial.Commands
{
    public class CloseDoorCommand: Command
    {
        [SerializeField] private Door door;
        public override string CommandID =>  nameof(OpenDoorCommand);
        
        public override void Execute()
        {
            base.Execute();   
            door.Close();
        }

        protected override bool CanExecute()
        {
            return door.CanMove(false);
        }
    }
}
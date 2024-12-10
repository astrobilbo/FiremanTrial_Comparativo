using FiremanTrial.Commands;
using UnityEngine;

namespace FiremanTrial.WithArchitecture
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
    }
}
using System;
using FiremanTrial.Commands;
using FiremanTrial.WithArchitecture;
using UnityEngine;

namespace FiremanTrial.Commands
{
    public class OpenDoorCommand : Command
    {
        [SerializeField] private Door door;
        public override string CommandID =>  nameof(OpenDoorCommand);
        
        public override void Execute()
        {
            base.Execute();   
            door.Open();
        }
        
        protected override bool CanExecute()
        {
            return door.CanMove(false);
        }
    }
}

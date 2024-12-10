using System;
using FiremanTrial.Commands;
using UnityEngine;

namespace FiremanTrial.WithArchitecture
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
    }
}

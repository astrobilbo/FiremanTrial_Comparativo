using System;
using FiremanTrial.WithArchitecture.Commands;
using UnityEngine;

namespace FiremanTrial.WithArchitecture
{
    public class OpenCloseDoorCommand : Command
    {
        [SerializeField] private Door door;
        public override string CommandID =>  nameof(OpenCloseDoorCommand);

        private void Start()
        {
            if (keyCode == KeyCode.None)
            {
                keyCode = KeyCode.F;
            }
        }

        private void Update()
        {
            if (keyCode != KeyCode.None && Input.GetKeyUp(keyCode)) Execute();
        }

        protected override void Execute()
        {
            if (door.CanOpen()) return;
            
            base.Execute();   
            door.OpenClose();
        }
        
    }
}

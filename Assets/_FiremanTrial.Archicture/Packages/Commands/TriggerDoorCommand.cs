using FiremanTrial.Object;
using UnityEngine;

namespace FiremanTrial.Commands
{
    public class TriggerDoorCommand : Command
    {
        [SerializeField] private Door door;
        public override string CommandID => nameof(TriggerDoorCommand);

        public override void Execute()
        {
            if (!CanExecute()) return;
            base.Execute();
            door.TriggerDoorMovement();
        }

        private bool CanExecute()
        {
            return door.CanMove();
        }
    }
}
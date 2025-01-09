using FiremanTrial.Object;
using UnityEngine;

namespace FiremanTrial.Commands
{
    public class OpenDoorCommand : Command
    {
        [SerializeField] private Door door;
        public override string CommandID => nameof(OpenDoorCommand);

        public override void Execute()
        {
            if (!CanExecute()) return;
            base.Execute();
            Debug.Log("Opening door");
            door.Open();
        }

        private bool CanExecute()
        {
            return door.CanMove(false);
        }

    }
}

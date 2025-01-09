using FiremanTrial.Commands;
using FiremanTrial.UI;
using UnityEngine;

namespace FiremanTrial.Commands
{
    public class TurnOnOffCellphoneCommand : Command
    {
        [SerializeField] private Cellphone cellphone;
        public override string CommandID => nameof(TurnOnOffCellphoneCommand);

        public override void Execute()
        {
            if (!CanExecute()) return;
            base.Execute();
            cellphone.TurnOnOffCellphone();
        }

        private bool CanExecute()
        {
            return true;
        }
    }
}
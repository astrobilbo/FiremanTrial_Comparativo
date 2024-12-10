using FiremanTrial.Commands;
using UnityEngine;

namespace FiremanTrial.UI
{
    public class TurnOnOffCellphoneCommand : Command
    {
        [SerializeField] private Cellphone cellphone;
        public override string CommandID => nameof(TurnOnOffCellphoneCommand);

        public override void Execute()
        {
            base.Execute();
            cellphone.TurnOnOffCellphone();
        }
    }
}
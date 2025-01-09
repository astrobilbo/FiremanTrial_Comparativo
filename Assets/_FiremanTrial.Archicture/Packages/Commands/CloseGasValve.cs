using FiremanTrial.Object;
using UnityEngine;

namespace FiremanTrial.Commands
{
    public class CloseGasValve : Command
    {
        [SerializeField] private Gas gas;
        public override string CommandID =>  nameof(CloseGasValve);

        public override void Execute()
        {
            if (!CanExecute()) return;
            base.Execute();
            gas.CloseValve();
        }

        private bool CanExecute()
        {
            return gas.CanInteract();
        }
    }
}
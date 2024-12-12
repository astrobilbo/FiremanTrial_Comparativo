using FiremanTrial.Commands;
using FiremanTrial.WithArchitecture;
using UnityEngine;

namespace FiremanTrial.Commands
{
    public class CloseGasValve : Command
    {
        [SerializeField] private Gas gas;
        public override string CommandID =>  nameof(CloseGasValve);

        public override void Execute()
        {
            base.Execute();
            gas.CloseValve();
        }

        protected override bool CanExecute()
        {
            return true;
        }
    }
}
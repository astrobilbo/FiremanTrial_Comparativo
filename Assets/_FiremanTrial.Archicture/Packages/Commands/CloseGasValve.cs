using FiremanTrial.Commands;
using UnityEngine;

namespace FiremanTrial.WithArchitecture
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
    }
}
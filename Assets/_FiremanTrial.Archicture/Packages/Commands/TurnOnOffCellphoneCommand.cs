using FiremanTrial.WithArchitecture.Commands;
using UnityEngine;

namespace FiremanTrial.UI
{
    public class TurnOnOffCellphoneCommand : Command
    {
        [SerializeField] private Cellphone cellphone;
        public override string CommandID => nameof(TurnOnOffCellphoneCommand);
        private void Start()
        {
            if (keyCode == KeyCode.None)
            {
                keyCode = KeyCode.Escape;
            }
        }  
        private void Update()
        {
            if (keyCode != KeyCode.None && Input.GetKeyUp(keyCode)) Execute();
        }
        protected override void Execute()
        {
            base.Execute();
            cellphone.TurnOnOffCellphone();
        }
    }
}
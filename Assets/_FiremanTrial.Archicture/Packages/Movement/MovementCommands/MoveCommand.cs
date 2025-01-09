using FiremanTrial.Movement;
using UnityEngine;
using UnityEngine.Serialization;

namespace FiremanTrial.Commands
{ 
    public class MoveCommand : Command
    {
        [FormerlySerializedAs("handler")] [FormerlySerializedAs("move")] [SerializeField] private MovementHandler movementHandler;
        [SerializeField] private MovementDirection direction;
        public override string CommandID => nameof(MoveCommand)+"_"+direction;

        public override void Execute() => movementHandler.AddMovementInput(direction);
    }
}
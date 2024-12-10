using FiremanTrial.Movement;
using UnityEngine;

namespace FiremanTrial.Commands.Movement
{
    public class VerticalCamRotationCommand : Command
    {
        [SerializeField] private CameraController movement;
        public override string CommandID => nameof(VerticalCamRotationCommand);
        
        public override void Execute(float value) => movement.VerticalRotation(value);
    }
}
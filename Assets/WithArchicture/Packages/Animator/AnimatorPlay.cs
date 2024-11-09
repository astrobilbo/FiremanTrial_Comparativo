using System.Collections.Generic;
using FiremanTrial.WithArchitecture.Commands;
using UnityEngine;

namespace FiremanTrial.WithArchitecture.Animator
{
    public class AnimatorPlay : AnimatorBase
    {
        [SerializeField] private List<Command> commandsToFollow;
        
        private void OnEnable()
        {
            foreach (var command in commandsToFollow) command.ActionExecuted += Play;
        }

        private void OnDisable()
        {
            foreach (var command in commandsToFollow) command.ActionExecuted -= Play;
        }
        public void Play() => animator?.Play(ParamID);
    }
}

using System.Collections.Generic;
using FiremanTrial.Commands;
using UnityEngine;

namespace FiremanTrial.WithArchitecture.Animator
{
    public class AnimatorPlay : AnimatorBase
    {
        [SerializeField] private List<Command> commandsToFollow;
        [SerializeField] private int currentAnimatorLayer;
        private void OnEnable()
        {
            foreach (var command in commandsToFollow) command.ActionExecuted += Play;
        }

        private void OnDisable()
        {
            foreach (var command in commandsToFollow) command.ActionExecuted -= Play;
        }
        public void Play() => animator?.Play(ParamID,currentAnimatorLayer);
    }
}

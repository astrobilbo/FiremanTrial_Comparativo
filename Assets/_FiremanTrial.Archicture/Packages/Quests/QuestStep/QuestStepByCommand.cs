using FiremanTrial.Commands;
using UnityEngine;

namespace FiremanTrial.Quest
{
    public class QuestStepByCommand: MonoBehaviour
    {
        public Command commandToFollow;
        public QuestStep questStep;
        private void OnEnable()
        {
            if (commandToFollow == null) return;
            commandToFollow.ActionExecuted += questStep.CompleteStep;
        }

        private void OnDisable()
        {
            if (commandToFollow == null) return;
            commandToFollow.ActionExecuted -= questStep.CompleteStep;
        }
    }
}
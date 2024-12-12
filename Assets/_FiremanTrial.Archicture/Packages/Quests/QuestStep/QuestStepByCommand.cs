using FiremanTrial.Commands;
using UnityEngine;

namespace FiremanTrial.Quest
{
    public class QuestStepByCommand: MonoBehaviour
    {
        public Command commandToFollow;
        public QuestStep questStep;
        private void OnEnable() => commandToFollow.ActionExecuted += questStep.CompleteStep;

        private void OnDisable() => commandToFollow.ActionExecuted -= questStep.CompleteStep;
    }
}
using FiremanTrial.WithArchitecture;
using UnityEngine;

namespace FiremanTrial.Quest
{
    public class QuestStepByPosition : MonoBehaviour
    {
        public SphereOverlapInteractions positionToGo;
        public QuestStep questStep;

        private void OnEnable()
        {
            if (positionToGo != null) positionToGo.ObjectInRange += questStep.CompleteStep;
        }

        private void OnDisable()
        {
            if (positionToGo != null) positionToGo.ObjectInRange -= questStep.CompleteStep;
        }
    }
}
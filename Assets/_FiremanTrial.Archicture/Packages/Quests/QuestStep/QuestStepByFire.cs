using FiremanTrial.Quest;
using FiremanTrial.WithArchitecture;
using UnityEngine;

public class QuestStepByFire : MonoBehaviour
{
    public Fire fire;
    public QuestStep questStep;
    private void OnEnable()
    {
        if (fire == null) return;
        fire.FireExtinguished += questStep.CompleteStep;
        fire.FirePassMaxValue += questStep.StepFailed;
    }

    private void OnDisable()
    {
        if (fire == null) return;
        fire.FireExtinguished -= questStep.CompleteStep;
        fire.FirePassMaxValue -= questStep.StepFailed;
    }
}

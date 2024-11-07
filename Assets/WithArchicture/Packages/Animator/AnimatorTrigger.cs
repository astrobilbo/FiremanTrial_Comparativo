namespace FiremanTrial.WithArchitecture
{
    public class AnimatorTrigger : AnimatorBase
    {
        public void Activate()
        {
            if (animator != null)
            {
                animator.SetTrigger(ParamID);
            }
        }

        public void Reset()
        {
            if (animator != null)
            {
                animator.ResetTrigger(ParamID);
            }
        }
    }
}
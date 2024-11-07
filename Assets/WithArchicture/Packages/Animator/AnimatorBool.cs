namespace FiremanTrial.WithArchitecture
{
    public class AnimatorBool : AnimatorBase
    {
        public void Toggle()
        {
            if (animator != null)
            {
                bool currentState = animator.GetBool(ParamID);
                animator.SetBool(ParamID, !currentState);
            }
        }

        public void Set(bool value)
        {
            if (animator != null)
            {
                animator.SetBool(ParamID, value);
            }
        }
    }
}
namespace FiremanTrial.WithArchitecture
{
    public class AnimatorInt : AnimatorBase
    {
        public void Set(int value)
        {
            if (animator != null)
            {
                animator.SetInteger(ParamID, value);
            }
        }
    }
}
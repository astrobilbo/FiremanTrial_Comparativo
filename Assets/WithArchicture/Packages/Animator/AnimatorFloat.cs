namespace FiremanTrial.WithArchitecture.Animator
{
    public class AnimatorFloat : AnimatorBase
    {
        public void Set(float value)
        {
            if (animator != null)
            {
                animator.SetFloat(ParamID, value);
            }
        }
    }
}
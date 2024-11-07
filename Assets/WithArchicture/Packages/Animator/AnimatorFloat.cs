namespace FiremanTrial.WithArchitecture
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
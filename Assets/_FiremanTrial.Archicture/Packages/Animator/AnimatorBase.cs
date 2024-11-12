using UnityEngine;

namespace FiremanTrial.WithArchitecture.Animator
{
    public abstract class AnimatorBase : MonoBehaviour
    {
        [SerializeField]protected UnityEngine.Animator animator;
        [SerializeField]protected string parameterName;
        protected int ParamID;

        public void Awake()
        {
            ParamID = UnityEngine.Animator.StringToHash(parameterName);
        }
    }
}
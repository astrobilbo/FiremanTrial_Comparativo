using UnityEngine;

namespace FiremanTrial.WithArchitecture
{
    public class AnimatorMovement : MonoBehaviour
    {
        [SerializeField] private Movement movement;
        [SerializeField] private UnityEngine.Animator animator;
        [SerializeField] private string animatorVerticalMovementFloatName="Vertical";
        [SerializeField] private string animatorHorizontalMovementFloatName="Horizontal";
        [SerializeField] private float smoothTime = 0.1f; 

        private int _verticalParamID;
        private int _horizontalParamID;
        private float _verticalVelocity;
        private float _horizontalVelocity; 
        private float _targetVerticalValue;
        private float _targetHorizontalValue;
        
        private void Awake()
        {
            _verticalParamID = UnityEngine.Animator.StringToHash(animatorVerticalMovementFloatName);
            _horizontalParamID = UnityEngine.Animator.StringToHash(animatorHorizontalMovementFloatName);
        }
        private void OnEnable()
        {
            if (movement == null) return;
            
            movement.Moving += UpdateMoveAnim;
            movement.Stoped += UpdateMoveAnim;
        }
        
        private void OnDisable()
        {
            if (movement == null) return;
                
            movement.Moving -= UpdateMoveAnim;
            movement.Stoped -= UpdateMoveAnim;
        }
        private void UpdateMoveAnim(Vector3 movementIntent)
        {
            _targetVerticalValue = movementIntent.z;
            _targetHorizontalValue = movementIntent.x;

            // Smoothly update animator parameters using SmoothDamp
            var vertical = Mathf.SmoothDamp(animator.GetFloat(animatorVerticalMovementFloatName), _targetVerticalValue, ref _verticalVelocity, smoothTime);
            var horizontal = Mathf.SmoothDamp(animator.GetFloat(animatorHorizontalMovementFloatName), _targetHorizontalValue, ref _horizontalVelocity, smoothTime);

            animator.SetFloat(_verticalParamID, vertical);
            animator.SetFloat(_horizontalParamID, horizontal);
        }
    }
}

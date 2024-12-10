using System.Collections;
using FiremanTrial.Movement;
using UnityEngine;

namespace FiremanTrial.MovementAnimator
{
    public class MoveAnimator : MonoBehaviour
    {
        private IMovementDirectionNotifier _observer;
        [SerializeField] private Animator animator;
        private const float SmoothTime = 0.1f; 
        private const string AnimatorVerticalMovementFloatName = "Vertical";
        private const string AnimatorHorizontalMovementFloatName = "Horizontal";
        private Coroutine _updateAnimatorCoroutine;
        private int _verticalParamID;
        private int _horizontalParamID;
        private float _verticalVelocity;
        private float _horizontalVelocity; 
        private float _targetVerticalValue;
        private float _targetHorizontalValue;
        
        public void Initialize(IMovementDirectionNotifier observer, Animator animator)
        {
            _observer = observer;
            this.animator = animator;
            _verticalParamID = Animator.StringToHash(AnimatorVerticalMovementFloatName);
            _horizontalParamID = Animator.StringToHash(AnimatorHorizontalMovementFloatName);
        }

        private void Awake()
        {
            _observer=GetComponent(typeof(IMovementDirectionNotifier)) as IMovementDirectionNotifier;
            _verticalParamID = Animator.StringToHash(AnimatorVerticalMovementFloatName);
            _horizontalParamID = Animator.StringToHash(AnimatorHorizontalMovementFloatName);
        }

        private void OnEnable() => _observer.DirectionObserver += OnMovementDirectionChanged;

        private void OnDisable() => _observer.DirectionObserver -= OnMovementDirectionChanged;

        private void OnMovementDirectionChanged(Vector3 movementIntent)
        {
            UpdateTargetValues(movementIntent);
            
            if (_updateAnimatorCoroutine != null)
            {
                StopCoroutine(_updateAnimatorCoroutine);
            }
            _updateAnimatorCoroutine = StartCoroutine(UpdateAnimatorParameters());
        }
        
        private void UpdateTargetValues(Vector3 movementIntent)
        {
            _targetVerticalValue = movementIntent.z;
            _targetHorizontalValue = movementIntent.x;
        }
        
        private IEnumerator UpdateAnimatorParameters()
        {
            while (!Mathf.Approximately(animator.GetFloat(_verticalParamID), _targetVerticalValue) ||
                   !Mathf.Approximately(animator.GetFloat(_horizontalParamID), _targetHorizontalValue))
            {
                var vertical = Mathf.SmoothDamp(animator.GetFloat(_verticalParamID), _targetVerticalValue, ref _verticalVelocity, SmoothTime);
                var horizontal = Mathf.SmoothDamp(animator.GetFloat(_horizontalParamID), _targetHorizontalValue, ref _horizontalVelocity, SmoothTime);

                animator.SetFloat(_verticalParamID, vertical);
                animator.SetFloat(_horizontalParamID, horizontal);

                yield return null; 
            }
            _updateAnimatorCoroutine = null;
        }
    }
}

using System.Collections;
using UnityEngine;

namespace FiremanTrial.Movement
{
    [RequireComponent(typeof(IMovementDirectionNotifier))]
    public class MoveAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private IMovementDirectionNotifier _observer;
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
        
        public void Awake()
        {
            _observer = GetComponent<MovementHandler>();
            _verticalParamID = Animator.StringToHash(AnimatorVerticalMovementFloatName);
            _horizontalParamID = Animator.StringToHash(AnimatorHorizontalMovementFloatName);
            SetObserver();
        }
        
        private void OnEnable() => SetObserver();

        private void OnDisable() => RemoveObserver();

        private void SetObserver()
        {
            if (_observer is null) return;
            _observer.DirectionObserver  += OnMovementDirectionChanged;
        }
        
        private void RemoveObserver()
        {
            if (_observer is null) return;
            _observer.DirectionObserver  -= OnMovementDirectionChanged;
        }
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

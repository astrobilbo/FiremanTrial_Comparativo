using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiremanTrial.WithArchitecture
{
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 1;
        [SerializeField] private float rotationSpeed = 10;
        [SerializeField] private float gravity = 10;
        [SerializeField]  private float verticalRotationLimitMax  = 90;
        [SerializeField] private float verticalRotationLimitMin  = -90;
        [SerializeField] private Animator animator;
        [SerializeField] private string animatorVerticalMovementFloatName="Vertical";
        [SerializeField] private string animatorHorizontalMovementFloatName="Horizontal";
        private CharacterController _characterController;
        private bool _active=true;
        private Vector3 _movementIntent = Vector3.zero;
        private Vector3 _targetRotation= Vector3.zero;
        private Camera _camera;
        
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _camera = Camera.main;
        }

        public bool Active
        {
            get => _active; 
            set => _active = value;
        }
        
        public void StartMove(MovementDirection direction)
        {
            _movementIntent += GetDirectionVector(direction);
        }

        public void StopMove(MovementDirection direction)
        {
            _movementIntent -= GetDirectionVector(direction);
        }

        private Vector3 GetDirectionVector(MovementDirection direction)
        {
            return direction switch
            {
                MovementDirection.Forward => Vector3.forward,
                MovementDirection.Backward => Vector3.back,
                MovementDirection.Left => Vector3.left,
                MovementDirection.Right => Vector3.right,
                _ => Vector3.zero
            };
        }

        public void HorizontalCharacterRotation(float direction) => UpdateCharacterRotation(direction);
        public void VerticalCamRotation(float direction) => UpdateCameraRotation(direction);
        
        private void UpdateCharacterRotation(float direction)
        {
            _characterController.transform.Rotate(Vector3.up, direction * rotationSpeed * Time.deltaTime);
        }
        
        private void UpdateCameraRotation(float direction)
        {
            _targetRotation.x -= direction;
            _targetRotation.x = Mathf.Clamp(_targetRotation.x, verticalRotationLimitMin, verticalRotationLimitMax);
            _camera.transform.localRotation = Quaternion.Euler(_targetRotation);
        }
      
        private void Update()
        {
            if (!_active) return;
            ChangePosition();
        }
        
        private void ChangePosition()
        {
            if (IsPlayerStationary) return;
            
            var moveDirection = CalculateMovement();
            moveDirection += GetGravityEffect();
            UpdateAnimator();
            _characterController.Move(moveDirection * Time.deltaTime);
        }
        
        private bool IsPlayerStationary => _movementIntent == Vector3.zero && _characterController.velocity == Vector3.zero && _characterController.isGrounded == true;
        
        private Vector3 CalculateMovement()
        {
            var verticalDirection = _characterController.transform.TransformDirection(Vector3.forward);
            var horizontalDirection = _characterController.transform.TransformDirection(Vector3.right);
            return (moveSpeed * (_movementIntent.x * horizontalDirection + _movementIntent.z * verticalDirection)).normalized;
        }
        
        private Vector3 GetGravityEffect()
        {
            return gravity * Vector3.down;
        }

        private void UpdateAnimator()
        {
            animator.SetFloat(animatorVerticalMovementFloatName, _movementIntent.z);
            animator.SetFloat(animatorHorizontalMovementFloatName, _movementIntent.x);
        }
    }
}
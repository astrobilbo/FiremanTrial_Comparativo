using System;
using UnityEngine;

namespace FiremanTrial.WithArchitecture
{
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        public event Action<Vector3> Moving;
        public event Action<Vector3> Stoped;
        [SerializeField] private float moveSpeed = 1;
        [SerializeField] private float rotationSpeed = 10;
        private CharacterController _characterController;
        private Vector3 _movementIntent = Vector3.zero;
        private bool _isMoving;
        private void Awake() => _characterController = GetComponent<CharacterController>();

        public void StartMove(MovementDirection direction)
        {
            if (!enabled)return;
            _isMoving = true;
            _movementIntent += GetDirectionVector(direction);
        }

        public void StopMove(MovementDirection direction)
        {
            if (!enabled)return;
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

        public void HorizontalCharacterRotation(float direction)
        {
            if (!enabled) return;
            UpdateCharacterRotation(direction);
        }

        private void UpdateCharacterRotation(float direction)
        {
            _characterController.transform.Rotate(Vector3.up, direction * rotationSpeed * Time.deltaTime);
        }
        
        private void Update()
        {
            ChangePosition();
        }
        
        private void ChangePosition()
        {
            if (IsPlayerStationary) return;

            var moveDirection = CalculateMovement();
            Moving?.Invoke(_movementIntent);
            _characterController.Move(moveDirection * Time.deltaTime);
        }
        
        private bool IsPlayerStationary
        {
            get
            {
                _isMoving= _movementIntent == Vector3.zero && _characterController.velocity == Vector3.zero && _characterController.isGrounded == true;
                if (!_isMoving) Stoped?.Invoke(_movementIntent);
                return _isMoving;
            }
        }

        private Vector3 CalculateMovement()
        {
            var verticalDirection = _characterController.transform.TransformDirection(Vector3.forward);
            var horizontalDirection = _characterController.transform.TransformDirection(Vector3.right);
            return (moveSpeed * (_movementIntent.x * horizontalDirection + _movementIntent.z * verticalDirection)).normalized;
        }
        
    }
}
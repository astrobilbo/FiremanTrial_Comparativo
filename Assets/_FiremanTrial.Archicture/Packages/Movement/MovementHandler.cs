using System;
using System.Collections.Generic;
using FiremanTrial.Manager;
using UnityEngine;

namespace FiremanTrial.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class MovementHandler : MonoBehaviour, IMovementDirectionNotifier, IMovingBooleanNotifier
    {
        public event Action<Vector3> DirectionObserver;        
        public event Action<bool> BooleanObserver;
        
        [SerializeField] private float speed = 1;
        [SerializeField] private float rotationRate = 10;
        private CharacterController _characterController;
        private Vector3 _desiredDirection = Vector3.zero;
        private bool _movementActive;
        private List<MovementDirection> _movingDirections = new List<MovementDirection>();
        bool _canMove = true;
        private void Awake() => _characterController = GetComponent<CharacterController>();

        private void FixedUpdate() => ApplyMovement();
        
        private void OnEnable() => GameManager.GameStateChanged += MovementReactionToGameStateChange;

        private void OnDisable() => GameManager.GameStateChanged -= MovementReactionToGameStateChange;

        //para o movimento se o estado do jogo nao for jogando
        private void MovementReactionToGameStateChange(GameState gameState)
        {
            if (gameState == GameState.Playing)
            {
                RestartMovement();
            }
            else
            {
                StopMovement();
            }
        }
        
        public void AddMovementInput(MovementDirection direction)
        {
            if (!_canMove) return;
            _movementActive = true;
            BooleanObserver?.Invoke(_movementActive);
            _desiredDirection += TranslateDirectionToVector(direction);
            _movingDirections.Add(direction);
        }

        public void RemoveMovementInput(MovementDirection direction)
        {
            if (!_canMove) return;
            _desiredDirection -= TranslateDirectionToVector(direction);
            _movingDirections.Remove(direction);
        }

        private Vector3 TranslateDirectionToVector(MovementDirection direction) =>
            direction switch
            {
                MovementDirection.Forward => Vector3.forward,
                MovementDirection.Backward => Vector3.back,
                MovementDirection.Left => Vector3.left,
                MovementDirection.Right => Vector3.right,
                _ => Vector3.zero,
            };
        
        public void HandleRotationInput(float direction) => RotateCharacter(direction);

        private void RotateCharacter(float direction)
        {
            if (!_canMove) return;

            _characterController.transform.Rotate(Vector3.up, UpdateRotationAngle(direction));
        }
        
        private float UpdateRotationAngle(float direction) => direction * rotationRate * Time.deltaTime;

        private void ApplyMovement()
        {
            if (!_canMove) return;
            if (!_movementActive) return;
            
            var isStationary = CheckIfStationary();
            if (isStationary) return;

            var moveDirection = ComputeMovementVector();
            DirectionObserver?.Invoke(_desiredDirection);
            
            _characterController.Move(moveDirection);
        }
        
        private bool CheckIfStationary()
        {
            _movementActive = !(_desiredDirection == Vector3.zero &&
                                _characterController.velocity == Vector3.zero);
                if (!_movementActive)
                {
                    DirectionObserver?.Invoke(_desiredDirection);
                    BooleanObserver?.Invoke(_movementActive);
                }
                return !_movementActive;
        }

        private Vector3 ComputeMovementVector()
        {
            var verticalDirection = _characterController.transform.TransformDirection(Vector3.forward);
            var horizontalDirection = _characterController.transform.TransformDirection(Vector3.right);
            return CalculateMovement(horizontalDirection, verticalDirection) ;
        }
        
        private Vector3 CalculateMovement(Vector3 horizontal, Vector3 vertical)
        {
            var direction = (_desiredDirection.x * horizontal + _desiredDirection.z * vertical).normalized;
            var speedInFrame = (speed * Time.fixedDeltaTime);
            var result =   direction * speedInFrame;
            return result;
        }

        public void StopMovement()
        {
            if (!_canMove) return;
            if (_movingDirections is null || _movingDirections.Count == 0) return;
            
            foreach (var activeMovingDirecion in _movingDirections)
            {
                RemoveMovementInput(activeMovingDirecion);
            }

            ApplyMovement();
            _canMove = false;
        }

        public void RestartMovement()
        {
            _canMove = true;
        }
    }
}
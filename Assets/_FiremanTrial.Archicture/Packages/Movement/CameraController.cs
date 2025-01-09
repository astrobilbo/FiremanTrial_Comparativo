using FiremanTrial.Manager;
using UnityEngine;

namespace FiremanTrial.Movement
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 10;
        [SerializeField] private float verticalRotationLimitMax = 90;
        [SerializeField] private float verticalRotationLimitMin = -90;
        private Vector3 _targetRotation = Vector3.zero;
        private Camera _camera;
        private bool _canRotate = true;
        private void Awake() => _camera = Camera.main;
        
        private void OnEnable()
        {
            GameManager.GameStateChanged += MovementReactionToGameStateChange;
        }

        private void OnDisable()
        {
            GameManager.GameStateChanged -= MovementReactionToGameStateChange;
        }

        private void MovementReactionToGameStateChange(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Playing:
                    _canRotate=true;
                    break;
                default:
                    _canRotate=false;
                    break;
            }
        }
        public void VerticalRotation(float direction) => RotateVertical(direction);

        private void RotateVertical(float direction)
        {
            if (!_canRotate) return;
            _targetRotation.x -= direction * rotationSpeed * Time.deltaTime;
            _targetRotation.x = Mathf.Clamp(_targetRotation.x, verticalRotationLimitMin, verticalRotationLimitMax);
            _camera.transform.localRotation = Quaternion.Euler(_targetRotation);
        }
    }
}
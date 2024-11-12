using UnityEngine;

namespace FiremanTrial.WithArchitecture
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 10;
        [SerializeField] private float verticalRotationLimitMax = 90;
        [SerializeField] private float verticalRotationLimitMin = -90;
        private Vector3 _targetRotation = Vector3.zero;
        private Camera _camera;

        private void Awake() => _camera = Camera.main;
        public void VerticalRotation(float direction) => RotateVertical(direction);

        private void RotateVertical(float direction)
        {
            _targetRotation.x -= direction * rotationSpeed * Time.deltaTime;
            _targetRotation.x = Mathf.Clamp(_targetRotation.x, verticalRotationLimitMin, verticalRotationLimitMax);
            _camera.transform.localRotation = Quaternion.Euler(_targetRotation);
        }
    }
}
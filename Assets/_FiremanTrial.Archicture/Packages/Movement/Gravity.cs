using System;
using UnityEngine;

namespace FiremanTrial.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class Gravity : MonoBehaviour
    {
        [SerializeField] private float gravity = 10;
        private CharacterController _characterController;

        private void Awake() => _characterController = GetComponent<CharacterController>();

        private void Update() => ApplyGravity();

        private void ApplyGravity() => _characterController.Move(CalculateGravity());

        private Vector3 CalculateGravity() => _characterController.isGrounded ? Vector3.zero : gravity * Vector3.down;
    }
}
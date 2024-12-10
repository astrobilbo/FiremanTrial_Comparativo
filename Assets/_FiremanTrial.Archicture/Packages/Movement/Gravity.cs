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

        private void Update() => _characterController.Move(ApplyGravity());

        private Vector3 ApplyGravity() => _characterController.isGrounded ? Vector3.zero : gravity * Vector3.down;
    }
}
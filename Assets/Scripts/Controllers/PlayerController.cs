using System;
using Interfaces;
using Systems;
using UnityEngine;

namespace Controllers
{
    public class PlayerController : MonoBehaviour, IPlayerMovement, IPlayerCamera
    {
        // Constant for player movement
        private const float gravity = 9.8f;
        
        // Player movement
        [SerializeField] private float speedWalk = 5f;
        [SerializeField] private float speedRun = 11f;
        [SerializeField] private float jumpForce = 6f;
        [SerializeField] private Vector3 Velocity;

        // Mouse properties
        [SerializeField] private float mouseSensitivity = 2f;
        [SerializeField] private float smoothTime = 0.5f;

        private float currentVelX;
        private float currentVelY;
        public Vector2 mousePos;
        private Vector2 CurMousePos;

        private Camera playerCamera;
        private CharacterController characterController;
        private HealthSystem healthSystem;
        
        private float Speed => Input.GetKey(KeyCode.LeftShift) ? speedRun : speedWalk;
        public bool IsJumping => !characterController.isGrounded || Input.GetKeyDown(KeyCode.Space);
        private Vector2 CameraMovementDirection => new(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        public Vector3 PlayerMovementDirection => new(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        
        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            playerCamera = Camera.main;
            
            if (!characterController || !playerCamera)
            {
                Debug.LogError("CharacterController or Camera not assigned!");
                enabled = false;
            }
        }

        private void Start()
        {
            healthSystem = new HealthSystem(100);
        }

        private void Update()
        {
            Move(PlayerMovementDirection);
            RotateCamera(CameraMovementDirection);
        }

        public void Move(Vector3 direction)
        {
            Jump();
            direction = transform.TransformDirection(direction) * Speed;
            direction.y = Velocity.y;
            characterController.Move(direction * Time.deltaTime);
        }
        
        public void Jump()
        {
            if (characterController.isGrounded)
            {
                Velocity.y = -1;
                if (Input.GetKeyDown(KeyCode.Space))
                    Velocity.y = jumpForce;
            }
            else
                Velocity.y -= gravity * 2 * Time.deltaTime;
        }
        
        public void RotateCamera(Vector2 direction)
        {
            mousePos += direction * mouseSensitivity;
            mousePos.y = Mathf.Clamp(mousePos.y, -50, 50);

            CurMousePos.x = Mathf.SmoothDamp(mousePos.x, CurMousePos.x, ref currentVelX, smoothTime);
            CurMousePos.y = Mathf.SmoothDamp(mousePos.y, CurMousePos.y, ref currentVelY, smoothTime);

            playerCamera.transform.rotation = Quaternion.Euler(-CurMousePos.y, CurMousePos.x, 0);
            RotatePlayer(CurMousePos);

            mousePos.x = mousePos.x > 360 || mousePos.x < -360 ? 0 : mousePos.x;
        }

        public void RotatePlayer(Vector2 direction)
        {
            transform.rotation = Quaternion.Euler(0, direction.x, 0);
        }
        
        public void TakeDamage(uint damage)
        {
            healthSystem.TakeDamage(damage);
        }
        
        public void Heal(uint heal)
        {
            healthSystem.Heal(heal);
        }
    }
}
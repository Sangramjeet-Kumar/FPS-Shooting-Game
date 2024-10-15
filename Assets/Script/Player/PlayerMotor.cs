using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;

    // Movement variables
    public float speed = 5f;  // Normal walking speed
    public float sprintMultiplier = 2f;  // Sprint speed multiplier
    public float crouchSpeedMultiplier = 0.5f;  // Crouch speed multiplier

    // Crouch variables
    public float crouchHeight = 1f;  // Height of the player when crouching
    public float standingHeight = 2f;  // Normal height of the player

    private bool isCrouching = false;
    private bool isGrounded;

    // Physics variables
    public float gravity = -9.8f;  // Gravity value
    public float jumpHeight = 1f;  // Height of the jump

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = controller.isGrounded;

        // Reset vertical velocity when grounded to prevent sinking
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        // Handle crouch input
        HandleCrouch();
    }

    // Process player movement based on input from InputManager.cs
    public void ProcessMove(Vector2 input)
    {
        // Set the base speed for this frame
        float currentSpeed = speed;

        // Apply sprint multiplier if Left Shift is pressed (and not crouching)
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            currentSpeed *= sprintMultiplier;
        }

        // Apply crouch speed multiplier if crouching
        if (isCrouching)
        {
            currentSpeed *= crouchSpeedMultiplier;
        }

        // Calculate movement direction
        Vector3 moveDirection = new Vector3(input.x, 0, input.y);
        controller.Move(transform.TransformDirection(moveDirection) * currentSpeed * Time.deltaTime);

        // Apply gravity to vertical velocity
        playerVelocity.y += gravity * Time.deltaTime;

        // Move the character vertically
        controller.Move(playerVelocity * Time.deltaTime);

        // Debug log for vertical velocity
        Debug.Log("Vertical Velocity: " + playerVelocity.y);
    }

    // Handle jumping logic
    public void Jump()
    {
        if (isGrounded)
        {
            // Calculate jump velocity
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    // Handle crouch logic
    private void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching;  // Toggle crouching state

            // Adjust player height based on crouch state
            if (isCrouching)
            {
                controller.height = crouchHeight;
            }
            else
            {
                controller.height = standingHeight;
            }
        }
    }
}
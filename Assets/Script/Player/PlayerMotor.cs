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

    // Audio variables
    public AudioSource audioSource;
    public AudioClip walkSound;
    public AudioClip sprintSound;
    public AudioClip crouchSound;
    public AudioClip jumpSound;


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
        float currentSpeed = speed;

        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            currentSpeed *= sprintMultiplier;

            // Play sprint sound
            if (!audioSource.isPlaying)
            {
                audioSource.clip = sprintSound;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else if (input.magnitude > 0) // Walking
        {
            // Play walking sound
            if (!audioSource.isPlaying || audioSource.clip != walkSound)
            {
                audioSource.clip = walkSound;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            // Stop audio when not moving
            audioSource.Stop();
        }

        if (isCrouching)
        {
            currentSpeed *= crouchSpeedMultiplier;

            // Play crouch sound
            if (!audioSource.isPlaying || audioSource.clip != crouchSound)
            {
                audioSource.clip = crouchSound;
                audioSource.loop = true;
                audioSource.Play();
            }
        }

        Vector3 moveDirection = new Vector3(input.x, 0, input.y);
        controller.Move(transform.TransformDirection(moveDirection) * currentSpeed * Time.deltaTime);

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }


    // Handle jumping logic
    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            // Play jump sound
            if (audioSource != null && jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }
    }


    // Handle crouch logic
    private void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;

            if (isCrouching)
            {
                controller.height = crouchHeight;

                // Play crouch sound
                if (!audioSource.isPlaying || audioSource.clip != crouchSound)
                {
                    audioSource.clip = crouchSound;
                    audioSource.loop = true;
                    audioSource.Play();
                }
            }
            else
            {
                controller.height = standingHeight;

                // Stop crouch sound
                if (audioSource.isPlaying && audioSource.clip == crouchSound)
                {
                    audioSource.Stop();
                }
            }
        }
    }

}
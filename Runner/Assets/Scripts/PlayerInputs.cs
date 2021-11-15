using System;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [Header("Jump Parameters")]
    [SerializeField, Range(0,3)]
    private float jumpHeight = 1f;

    [SerializeField, Range(1, 5)]
    private float SpeedGoingDown = 3f;

    [SerializeField, Range(1, 5)]
    private float SpeedGoingUp = 1.5f;

    private CharacterController characterController;
    private Vector3 playerVelocity;
    private const float GRAVITY_VALUE = -9.81f;
    private bool isStartJump = false;
    private bool speedJumpDown = false;
    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleJump();
    }

    private void HandleJumpDown()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isStartJump)
        {
            speedJumpDown = true;
            playerVelocity.y = -0.01f;
            
        }

        if (speedJumpDown)
            playerVelocity.y += GRAVITY_VALUE * SpeedGoingDown * 5 * Time.deltaTime;

    }

    private void HandleJump()
    {
        IsGrounded = characterController.isGrounded;

        if (isStartJump)
            isStartJump = false;
        
        if (IsGrounded && playerVelocity.y < 0)
            playerVelocity.y = 0f;

        if (IsGrounded && speedJumpDown)
            speedJumpDown = false;


        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            isStartJump = true;
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * GRAVITY_VALUE);
        }
          

        bool isGoingDown = playerVelocity.y < 0;

        HandleJumpDown();

        if(!speedJumpDown)
            playerVelocity.y += isGoingDown ? 
            GRAVITY_VALUE * SpeedGoingDown * Time.deltaTime : 
            GRAVITY_VALUE * SpeedGoingUp * Time.deltaTime;

        characterController.Move(playerVelocity * Time.deltaTime);
    }

}

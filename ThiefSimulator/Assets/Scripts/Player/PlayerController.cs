using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    InputManager inputManager;
    PlayerManager playerManager;

    public Rigidbody playerRigidbody;

    [Header("Camera Location")]
    public Transform cameraHolderTransform;

    [Header("Movement Speed")]
    public float rotationSpeed = 3.5f;
    public float turnSpeed = 8;

    [Header("Jump Settings")]
    public float jumpHeight = 5f; // Height of the jump
    public bool isJumping = false; // Whether the player is currently jumping
    public bool isComingDown = false; // Whether the player is descending

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        inputManager = GetComponent<InputManager>();
        playerManager = GetComponent<PlayerManager>();
    }

    public void ManageAllMovement()
    {
        ManageRotation();
        HandleJump(); // Handle jump within the movement logic
    }

    private void ManageRotation()
    {
        if (playerManager.isAimingGun)
        {
            Quaternion targetRotation = Quaternion.Euler(0, cameraHolderTransform.eulerAngles.y, 0);
            Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = playerRotation;
        }
        else
        {
            Quaternion targetRotation = Quaternion.Euler(0, cameraHolderTransform.eulerAngles.y, 0);
            Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (inputManager.verticalMovementInput != 0 || inputManager.horizontalMovementInput != 0)
            {
                transform.rotation = playerRotation;
            }

            if (playerManager.isPerformingTurn)
            {
                playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
                transform.rotation = playerRotation;
            }
        }
    }

    private void HandleJump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!isJumping)
            {
                isJumping = true;
                // Trigger jump animation
                playerManager.animationController.animator.Play("Jump");
                StartCoroutine(JumpSequence());
            }
        }

        // Move player up or down during jump
        if (isJumping)
        {
            if (!isComingDown)
            {
                transform.Translate(Vector3.up * Time.deltaTime * jumpHeight, Space.World);
            }
            else
            {
                transform.Translate(Vector3.up * Time.deltaTime * -jumpHeight, Space.World);
            }
        }
    }

    private IEnumerator JumpSequence()
    {
        // Simulate ascending
        yield return new WaitForSeconds(0.45f);
        isComingDown = true;

        // Simulate descending
        yield return new WaitForSeconds(0.45f);
        isJumping = false;
        isComingDown = false;

        // Return to standard movement by playing the "Empty" animation state
        playerManager.animationController.animator.Play("Empty");
    }
}




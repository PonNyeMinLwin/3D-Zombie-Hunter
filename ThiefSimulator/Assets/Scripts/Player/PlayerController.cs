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
    public float jumpHeight = 5f; 
    public bool isJumping = false; 
    public bool isComingDown = false; 

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        inputManager = GetComponent<InputManager>();
        playerManager = GetComponent<PlayerManager>();
    }

    public void ManageAllMovement()
    {
        ManageRotation();
        ManageJump(); 
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

    private void ManageJump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!isJumping)
            {
                isJumping = true;
                // Play jump animation
                playerManager.animationController.animator.Play("Jump");
                StartCoroutine(JumpSequence());
            }
        }

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
        // Jumping
        yield return new WaitForSeconds(0.45f);
        isComingDown = true;

        // Falling
        yield return new WaitForSeconds(0.45f);
        isJumping = false;
        isComingDown = false;

        playerManager.animationController.animator.Play("Empty");
    }
}




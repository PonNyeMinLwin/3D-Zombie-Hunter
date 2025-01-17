using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private InputManager inputManager;
    private PlayerManager playerManager;

    public Transform cameraPivot;
    public Camera cameraObject;

    [Header("TPC Camera Follow Targets")]
    public GameObject target; // Follows the player when they're not aiming
    public Transform aimDownSightTarget; // Follows the position of the aim target when player is aiming

    Vector3 cameraSpeed = Vector3.zero;
    Vector3 targetPosition;
    Vector3 cameraRotation;
    Quaternion targetRotation;

    float cameraVerticalPivot;
    float cameraHorizontalPivot;
    float maxPivotAngle = 15;
    float minPivotAngle = -15;

    [Header("Camera Smoothness")]
    public float cameraSmoothTime = 0.5f;
    public float aimDownSightCameraSmoothTime = 2f;

    private void Awake() {
        inputManager = target.GetComponent<InputManager>();
        playerManager = target.GetComponent<PlayerManager>();
    }

    public void AllCameraMovements() {
        FollowTarget();
        RotateCamera();
    }

    private void FollowTarget() {
        if (playerManager.isAimingGun) {
            targetPosition = Vector3.SmoothDamp(transform.position, aimDownSightTarget.transform.position, ref cameraSpeed, cameraSmoothTime * Time.deltaTime);
            transform.position = targetPosition;
        }
        else {
            targetPosition = Vector3.SmoothDamp(transform.position, target.transform.position, ref cameraSpeed, cameraSmoothTime * Time.deltaTime);
            transform.position = targetPosition;
        }
    }

    private void RotateCamera() {
        if (playerManager.isAimingGun) {
            cameraPivot.localRotation = Quaternion.Euler(0, 0, 0);

            cameraVerticalPivot = cameraVerticalPivot + (inputManager.horizontalCameraInput);
            cameraHorizontalPivot = cameraHorizontalPivot - (inputManager.verticalCameraInput);
            cameraHorizontalPivot = Mathf.Clamp(cameraHorizontalPivot, minPivotAngle, maxPivotAngle);
            
            // Left and right camera pivot movement
            cameraRotation = Vector3.zero;
            cameraRotation.x = cameraHorizontalPivot;
            targetRotation = Quaternion.Euler(cameraRotation);
            targetRotation = Quaternion.Slerp(cameraPivot.localRotation, targetRotation, aimDownSightCameraSmoothTime);
            cameraObject.transform.localRotation = targetRotation;

            // Up and down camera pivot movement
            cameraRotation = Vector3.zero;
            cameraRotation.y = cameraVerticalPivot;
            targetRotation = Quaternion.Euler(cameraRotation);
            targetRotation = Quaternion.Slerp(transform.rotation, targetRotation, aimDownSightCameraSmoothTime);
            transform.rotation = targetRotation;
        }
        else {
            cameraObject.transform.localRotation = Quaternion.Euler(0, 0, 0);

            cameraVerticalPivot = cameraVerticalPivot + (inputManager.horizontalCameraInput);
            cameraHorizontalPivot = cameraHorizontalPivot - (inputManager.verticalCameraInput);
            cameraHorizontalPivot = Mathf.Clamp(cameraHorizontalPivot, minPivotAngle, maxPivotAngle);
            
            // Left and right camera pivot movement
            cameraRotation = Vector3.zero;
            cameraRotation.x = cameraHorizontalPivot;
            targetRotation = Quaternion.Euler(cameraRotation);
            targetRotation = Quaternion.Slerp(cameraPivot.localRotation, targetRotation, cameraSmoothTime);
            cameraPivot.localRotation = targetRotation;
              
            // When performing a turn, the camera will turn with the target 180 degrees
            if (inputManager.turnInput) {
                inputManager.turnInput = false;
                cameraVerticalPivot = cameraVerticalPivot + 180;
                cameraRotation.y = cameraRotation.y + 180;
                transform.rotation = targetRotation;
            }
            
            // Up and down camera pivot movement
            cameraRotation = Vector3.zero;
            cameraRotation.y = cameraVerticalPivot;
            targetRotation = Quaternion.Euler(cameraRotation);
            targetRotation = Quaternion.Slerp(transform.rotation, targetRotation, cameraSmoothTime);
            transform.rotation = targetRotation;
        }
    }
}

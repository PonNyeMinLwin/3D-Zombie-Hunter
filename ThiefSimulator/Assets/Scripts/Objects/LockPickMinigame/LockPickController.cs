using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPickController : MonoBehaviour
{
    [SerializeField] private float pickSpeed = 3f;
    [SerializeField] private float lockRotationSpeed = 0.4f;
    [SerializeField] private float lockPullBackForce = 0.2f;
    [SerializeField] private float pickStrength = 1f;
    [SerializeField] private float luckFactor = 0.1f;
    private float pickPosition;
    private float interiorLockPosition;
    private float randomTargetPosition; 

    private float pickTension = 0f;

    
    private Animator animator;

    private bool isPaused = false;
    private bool isShaking;
    
    // Ensures the lockpick's position only goes from ranges 0 -> 1
    public float PickPosition {
        get { return pickPosition; }
        set {
            pickPosition = value;
            pickPosition = Mathf.Clamp(pickPosition, 0f, 1f);
        }
    }

    // Ensures the lock's position only goes from ranges 0 -> 1
    public float InteriorLockPosition {
        get { return interiorLockPosition; }
        set {
            interiorLockPosition = value;
            interiorLockPosition = Mathf.Clamp(interiorLockPosition, 0f, MaxRotationDistance);
        }
    }

    // Limits pick movement according to target win condition 
    private float MaxRotationDistance {
        get { return 1f - Mathf.Abs(randomTargetPosition - PickPosition) + luckFactor; }
    }

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        Init();
    }
    
    // Initiliaises the starting position and values of GameObjects
    private void Init() {
        ResetMinigame();
    }

    // Resets minigame 
    private void ResetMinigame() {
        InteriorLockPosition = 0;
        PickPosition = 0;
        pickTension = 0f;
        isPaused = false;

        // Randomises win condition
        randomTargetPosition = UnityEngine.Random.value;
    }

    // Run these functions every frame if the game is not paused
    private void Update() {
        if (isPaused == true) { return; }
        if (Input.GetAxisRaw("Vertical") == 0) {
            UpdatePickPosition();
        }
        PickShake();
        UpdateInteriorLockPosition();
        UpdateAnimator();
    }

    // Gets the input of the player and changes location of the lockpick
    private void UpdatePickPosition() {
        PickPosition += Input.GetAxisRaw("Horizontal") * Time.deltaTime * pickSpeed;
    } 

    // Gets the input of the player and changes location of the interior lock
    private void UpdateInteriorLockPosition() {
        InteriorLockPosition -= lockPullBackForce * Time.deltaTime;
        InteriorLockPosition += Mathf.Abs(Input.GetAxisRaw("Vertical")) * Time.deltaTime * lockRotationSpeed;
        // If a specific position is reached, open interior lock
        if (InteriorLockPosition > 0.98f) {
            OpenInteriorLock();
        }
    }

    // Updates animation scenes according to player movement
    private void UpdateAnimator() {
        animator.SetFloat("PickRotation", PickPosition);
        animator.SetFloat("LockOpen", InteriorLockPosition);
        animator.SetBool("PickShake", isShaking);
    }

    // Shakes the pick - needs audio 
    private void PickShake() {
        isShaking = MaxRotationDistance - InteriorLockPosition < 0.03f;
        // If the pick shakes too much, it will break
        if (isShaking) {
            pickTension += Time.deltaTime * pickStrength;
            if (pickTension > 1f) {
                LockPickBreak();
            }
        }
    }

    // Win condition: unlocks chest (for now)
    // Add audio in this function 
    private void OpenInteriorLock() {
        isPaused = true;
        Debug.Log("You opened the interior lock!");
    }

    // Lose condition: player loses lockpick from inventory
    // Add audio in this function
    private void LockPickBreak() {
        Debug.Log("You broke the pick. You missed out on the loot!");
        isPaused = true;
        //Need to add a co-routine to add a delay before resetting!!!!!
        //ResetMinigame();
    }
}

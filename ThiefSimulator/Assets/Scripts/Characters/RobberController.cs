using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberController : MonoBehaviour
{
    public GameObject playerCharacter;
    public float jumpHeight = 5;
    public bool isRunning = false;
    public bool isJumping = false;
    public bool isComingDown = false;
    public float horizontalMovement;
    public float verticalMovement;
    
    void Update() {
        //Making the character move with their animations 
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical")) {
            playerCharacter.GetComponent<Animation>().Play("Run");
            horizontalMovement = Input.GetAxis("Horizontal") * Time.deltaTime * 150;
            verticalMovement = Input.GetAxis("Vertical") * Time.deltaTime * 8;
            isRunning = true;
            transform.Rotate(0, horizontalMovement, 0);
            transform.Translate(0, 0, verticalMovement);
        }
        //Jumping
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space)) {
            if (isJumping == false) {
                isJumping = true;
                //Getting the jump animation
                playerCharacter.GetComponent<Animation>().Play("Jump");
                StartCoroutine(JumpSequence());
                }
        }
        //Monitoring where the player is while jumping
        if (isJumping == true) {
            if (isComingDown == false) {
                transform.Translate(Vector3.up * Time.deltaTime * jumpHeight, Space.World);
            }
            else {
                transform.Translate(Vector3.up * Time.deltaTime * -jumpHeight, Space.World);
            }
        }
    }
    IEnumerator JumpSequence() {
        //Starting the jump and replaying the idle animation when it's done
        yield return new WaitForSeconds(0.3f);
        isComingDown = true;
        yield return new WaitForSeconds(0.3f);
        isJumping = false;
        isComingDown = false;
        playerCharacter.GetComponent<Animation>().Play("Ninja Idle");
        isRunning = false;
    }     
}

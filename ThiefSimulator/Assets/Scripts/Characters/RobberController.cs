using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberController : MonoBehaviour
{
    public GameObject playerCharacter;
    public bool isMoving;
    public float horizontalMovement;
    public float verticalMovement;

    void Update() {
        //Making the character move with their animations 
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical")) {
            playerCharacter.GetComponent<Animation>().Play("Run");
            horizontalMovement = Input.GetAxis("Horizontal") * Time.deltaTime * 150;
            verticalMovement = Input.GetAxis("Vertical") * Time.deltaTime * 8;
            isMoving = true;
            transform.Rotate(0, horizontalMovement, 0);
            transform.Translate(0, 0, verticalMovement);
        }
        else {
            playerCharacter.GetComponent<Animation>().Play("Ninja Idle");
            isMoving = false;
        }
    }
}

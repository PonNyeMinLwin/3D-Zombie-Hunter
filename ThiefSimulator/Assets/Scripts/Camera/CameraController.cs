using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int rotateSpeed = 100;

    void Update() {
        //Moving the camera after detecting the position of the mouse 
        if (Input.GetAxis("Mouse X") < 0) {
            transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
        }
        if (Input.GetAxis("Mouse X") > 0) {
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }   
    }
}

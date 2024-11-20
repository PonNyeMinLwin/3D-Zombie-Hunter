using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TextPrompter : MonoBehaviour
{
    public GameObject textPrompt;
    
    // Ensuring the text prompt only appears when player gets near objects
    private void Start() {
        textPrompt.SetActive(false);
    }
    
    // If text prompt is active and player clicks E, load the lock-picking minigame 
    private void OnTriggerEnter(Collider other) {
        textPrompt.SetActive(true);
    }

    private void OnTriggerExit() {
        textPrompt.SetActive(false);
    }
}

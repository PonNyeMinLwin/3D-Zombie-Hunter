using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestController : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt; 

    public bool Interact(Interactor interactor) {
        Debug.Log("Chest opened - minigame starting");
        SceneManager.LoadScene(2);
        return true;
    }
}

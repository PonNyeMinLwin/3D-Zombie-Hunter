using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Ensures only one of this variable exists when game runs and make it accessible in all classes
    public static MainMenuManager Singleton; 

    [SerializeField] private int gameSceneIndex = 1;

    private void Awake() {
        // Destroy other instances
        if (Singleton == null) { Singleton = this; }
        else { Destroy(gameObject); }
    }

    private void Start() {
        DontDestroyOnLoad(gameObject);
    }
    
    // Wait for a few seconds before starting coroutine
    public IEnumerator LoadNewGame() {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(gameSceneIndex);

        yield return null;
    }
}

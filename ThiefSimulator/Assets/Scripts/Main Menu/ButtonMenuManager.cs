using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMenuManager : MonoBehaviour
{
    public void StartNewGame() {
        StartCoroutine(MainMenuManager.Singleton.LoadNewGame());
    }
}

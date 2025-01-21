using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneUIManager : MonoBehaviour
{
    public GameObject fadeInScreen;
    
    private void Start() {
        fadeInScreen.SetActive(true);
        StartCoroutine(CutSceneSequence());
    }

    IEnumerator CutSceneSequence() {
        yield return new WaitForSeconds(1f);
        fadeInScreen.SetActive(false);
    }
}

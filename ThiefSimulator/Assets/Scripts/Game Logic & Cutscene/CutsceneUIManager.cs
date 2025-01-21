using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutsceneUIManager : MonoBehaviour
{
    [Header("Black Fade In Screen")]
    public GameObject fadeInScreen;

    [Header("Subtitle Box")]
    public GameObject textBox;

    // Thinking of fading in the location and the SURVIVE text box too
    
    private void Start() {
        fadeInScreen.SetActive(true);
        
        // Going to control subtitles with co-routines because it's easier
        StartCoroutine(CutSceneSequence());
    }

    IEnumerator CutSceneSequence() {
        yield return new WaitForSeconds(1f);
        fadeInScreen.SetActive(false);

        yield return new WaitForSeconds(2f);
        textBox.GetComponent<TextMeshProUGUI>().text = "The world changed when the zombies came.";

        yield return new WaitForSeconds(5f);
        textBox.GetComponent<TextMeshProUGUI>().text = "";

        yield return new WaitForSeconds(1f);
        textBox.GetComponent<TextMeshProUGUI>().text = "Houses burned. Cities collapsed. Humanity dwindled";

        yield return new WaitForSeconds(5f);
        textBox.GetComponent<TextMeshProUGUI>().text = "";

        yield return new WaitForSeconds(1f);
        textBox.GetComponent<TextMeshProUGUI>().text = "When dusk breaks and the moonlight peeks through the dusty clouds...";

        yield return new WaitForSeconds(5f);
        textBox.GetComponent<TextMeshProUGUI>().text = "";

        yield return new WaitForSeconds(1f);
        textBox.GetComponent<TextMeshProUGUI>().text = "The undead noises whispers in your ear";

        yield return new WaitForSeconds(5f);
        textBox.GetComponent<TextMeshProUGUI>().text = "";

        yield return new WaitForSeconds(1f);
        textBox.GetComponent<TextMeshProUGUI>().text = "But, I will not cower, nor will I waver";

        yield return new WaitForSeconds(5f);
        textBox.GetComponent<TextMeshProUGUI>().text = "";

        yield return new WaitForSeconds(1f);
        textBox.GetComponent<TextMeshProUGUI>().text = "For I am Joe!";

        yield return new WaitForSeconds(5f);
        textBox.GetComponent<TextMeshProUGUI>().text = "";

        yield return new WaitForSeconds(1f);
        textBox.GetComponent<TextMeshProUGUI>().text = "Joe: The Zombie Hunter!";
    }
}

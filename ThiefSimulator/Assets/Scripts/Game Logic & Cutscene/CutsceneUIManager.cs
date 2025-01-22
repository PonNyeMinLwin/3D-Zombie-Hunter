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
        textBox.GetComponent<TextMeshProUGUI>().text = "On this clear, starless night...";

        yield return new WaitForSeconds(5f);
        textBox.GetComponent<TextMeshProUGUI>().text = "";

        yield return new WaitForSeconds(1f);
        textBox.GetComponent<TextMeshProUGUI>().text = "The zombies arrived, violent and starving.";

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
        textBox.GetComponent<TextMeshProUGUI>().text = "I must get to the house as quickly as possible.";

        yield return new WaitForSeconds(5f);
        textBox.GetComponent<TextMeshProUGUI>().text = "";

        yield return new WaitForSeconds(1f);
        textBox.GetComponent<TextMeshProUGUI>().text = "My wife. My kids. I need to go find them!";

        yield return new WaitForSeconds(5f);
        textBox.GetComponent<TextMeshProUGUI>().text = "";

        yield return new WaitForSeconds(1f);
        textBox.GetComponent<TextMeshProUGUI>().text = "I swear on my name Joe, I will find them!";

        yield return new WaitForSeconds(5f);
        textBox.GetComponent<TextMeshProUGUI>().text = "";

        yield return new WaitForSeconds(1f);
        textBox.GetComponent<TextMeshProUGUI>().text = "I'll kill every zombie on the planet if that helps me find them.";

        yield return new WaitForSeconds(5f);
        textBox.GetComponent<TextMeshProUGUI>().text = "";

        yield return new WaitForSeconds(1f);
        textBox.GetComponent<TextMeshProUGUI>().text = "Where could they be!?";

        yield return new WaitForSeconds(7f);
        textBox.GetComponent<TextMeshProUGUI>().text = "";

        yield return new WaitForSeconds(1f);
        textBox.GetComponent<TextMeshProUGUI>().text = "The house looks empty but the lights are on.";

        yield return new WaitForSeconds(5f);
        textBox.GetComponent<TextMeshProUGUI>().text = "";

        yield return new WaitForSeconds(1f);
        textBox.GetComponent<TextMeshProUGUI>().text = "It's completely trashed! Was it zombies?";

        yield return new WaitForSeconds(5f);
        textBox.GetComponent<TextMeshProUGUI>().text = "";

        yield return new WaitForSeconds(1f);
        textBox.GetComponent<TextMeshProUGUI>().text = "But no bodies. Or blood.";

        yield return new WaitForSeconds(20f);
        textBox.GetComponent<TextMeshProUGUI>().text = "";

        yield return new WaitForSeconds(1f);
        textBox.GetComponent<TextMeshProUGUI>().text = "My wife's chair! They must've been taken!";

        yield return new WaitForSeconds(10f);
        textBox.GetComponent<TextMeshProUGUI>().text = "";
    }
}

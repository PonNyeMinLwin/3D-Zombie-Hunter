using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    private PlayerManager player;

    [Header("Heart Counter Display")]
    public GameObject livesCounterDisplay;

    [Header("UI Heart Displays")]
    public GameObject heartOne; 
    public GameObject heartTwo;
    public GameObject heartThree;
    // This variable will be controlled by the ObstacleCollider script or this one?
    public static int health = 3;

    private void Awake() {
        player = GetComponent<PlayerManager>();

        // Set all hearts to active in game UI
        heartOne.gameObject.SetActive(true);
        heartTwo.gameObject.SetActive(true);
        heartThree.gameObject.SetActive(true);
    }

    private void Update() {
        switch (health) {
            case 3:
                heartOne.gameObject.SetActive(true);
                heartTwo.gameObject.SetActive(true);
                heartThree.gameObject.SetActive(true);
                break;
            case 2:
                heartOne.gameObject.SetActive(false);
                heartTwo.gameObject.SetActive(true);
                heartThree.gameObject.SetActive(true);
                break;
            case 1: 
                heartOne.gameObject.SetActive(false);
                heartTwo.gameObject.SetActive(false);
                heartThree.gameObject.SetActive(true);
                break;
            case 0:
                heartOne.gameObject.SetActive(false);
                heartTwo.gameObject.SetActive(false);
                heartThree.gameObject.SetActive(false);
                livesCounterDisplay.gameObject.SetActive(false);
                break;
        }
    }

    private void KillPlayer() {
        if (!player.isPerformingInput) {
            player.animationController.PlayAnimationWithRootMotions("Death", true);
        }
        player.isDead = true;
    }

    public void TakesDamageFromSwipe() {
        health = health - 1;

        if (health == 0) {
            KillPlayer();
        }
    }

}

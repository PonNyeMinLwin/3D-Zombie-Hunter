using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthManager : MonoBehaviour
{
    private PlayerManager player;
    private GameLogicManager gameLogicManager;
    private PlayerUIManager playerUIManager;

    [Header("Heart Counter Display")]
    public GameObject livesCounterDisplay;

    [Header("UI Heart Displays")]
    public GameObject heartOne;
    public GameObject heartTwo;
    public GameObject heartThree;

    public static int health = 3;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
        gameLogicManager = FindObjectOfType<GameLogicManager>();
        playerUIManager = FindObjectOfType<PlayerUIManager>();

        health = 3;
        
        // Set all hearts to active in game UI
        livesCounterDisplay.gameObject.SetActive(true);
        heartOne.gameObject.SetActive(true);
        heartTwo.gameObject.SetActive(true);
        heartThree.gameObject.SetActive(true);
    }

    private void Update()
    {
        // Update heart display based on health
        switch (health)
        {
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

    public void TakesDamageFromSwipe()
    {
        // Decrease health when damaged
        health -= 1;

        // Check if health is zero
        if (health == 0)
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        // Trigger the death animation
        if (!player.isPerformingInput)
        {
            player.animationController.PlayAnimationWithRootMotions("Death", true);
        }

        // Mark the player as dead
        player.isDead = true;

        // Start the death sequence to handle End Game UI
        StartCoroutine(HandleDeathSequence());
    }

    private IEnumerator HandleDeathSequence()
    {
        // Wait for the death animation to finish (adjust timing to match animation length)
        yield return new WaitForSeconds(3f);

        // Trigger the End Game UI
        TriggerEndGameUI();
    }

    private void TriggerEndGameUI()
    {
        if (playerUIManager != null && gameLogicManager != null)
        {
            // Disable the in-game UI
            livesCounterDisplay.SetActive(false);
            playerUIManager.inGameDisplay.SetActive(false); 

            // Enable the End Game UI
            playerUIManager.endGameDisplay.SetActive(true);

            // Update total kills and rounds survived in the End Game UI
            playerUIManager.totalKillsText.text = gameLogicManager.totalKills.ToString();
            playerUIManager.totalRoundsText.text = gameLogicManager.roundsSurvived.ToString();
        }
        else
        {
            Debug.LogWarning("PlayerUIManager or GameLogicManager not found. Cannot display End Game UI.");
        }
    }
}


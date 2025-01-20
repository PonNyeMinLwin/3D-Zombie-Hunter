using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameLogicManager : MonoBehaviour
{
    public PlayerUIManager playerUIManager;

    [Header("Enemies Alive In Game Scene")]
    public int enemiesAliveCurrently;

    [Header("Rounds Survived")]
    public int roundsSurvived;

    [Header("Spawn Points")]
    public GameObject[] mobSpawnPoints;

    [Header("Enemies To Spawn")]
    public GameObject enemyPrefab;

    [Header("Kill Counters")]
    public int totalKills;
    public int amountOfKillsThisRound;


    private void Start() {
        // Initializes the first round
        roundsSurvived = 0;
        amountOfKillsThisRound = 0;
        UpdatePlayerUI();
        NextRound(roundsSurvived);
    }

    private void Update() {
        // If there are no enemies in scene, player advances to next round
        if (enemiesAliveCurrently == 0) {
            roundsSurvived++;
            amountOfKillsThisRound = 0; // Needs to reset this kill counter 
            UpdatePlayerUI();
            NextRound(roundsSurvived);
        }
    }

    // (Temporary function) - to instantiate one mob in one of any possible spawn locations per round - eg. R1 = 1 mob, R2 = 2 mob
    // (Future function) - make a script to note down how many mobs should appear every round 
    // (Future function) - make a function to choose between different types of enemies - eg. zombie, vampire 
    public void NextRound(int roundsSurvived) {
        for (var x = 0; x < roundsSurvived; x++) {
            // Take a random spawn point from the different mob spawners
            GameObject spawnPoint = mobSpawnPoints[Random.Range(0, mobSpawnPoints.Length)];
            
            // Instantiates an enemy (zombie) on the position of the mob spawner
            // Get the ZombieHealthManager script from this cloned zombie (temporary hotfix)
            GameObject spawnedZombie = Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
            // Makes it so that we don't need to properly reference it - we just get the clone's script for a while
            spawnedZombie.GetComponent<ZombieHealthManager>().gameLogicManager = GetComponent<GameLogicManager>();
            enemiesAliveCurrently++;
        }
        // Updates UI
        UpdatePlayerUI();
    }

    public void ManageZombieKillCount() {
        amountOfKillsThisRound++;
        totalKills++; // Will show at Restart Game UI
        enemiesAliveCurrently--;

        UpdatePlayerUI();
    }

    private void UpdatePlayerUI() {
        if (playerUIManager != null) {
            // Changes the Rounds Survived Counter Display
            if (playerUIManager.roundsSurvivedCountText != null) {
                playerUIManager.roundsSurvivedCountText.text = roundsSurvived.ToString();
            }

            // Changes the Kills This Round Counter Display
            if (playerUIManager.currentKillsCountText != null) {
                playerUIManager.currentKillsCountText.text = amountOfKillsThisRound.ToString();
            }

            // Changes the Zombies Left This Round Counter Display
            if (playerUIManager.zombiesLeftInRoundCountText != null) {
                playerUIManager.zombiesLeftInRoundCountText.text = enemiesAliveCurrently.ToString();
            }
        } else {
            Debug.Log("PlayerUIManager not accessed by GameLogicManager");
        }
    }
}

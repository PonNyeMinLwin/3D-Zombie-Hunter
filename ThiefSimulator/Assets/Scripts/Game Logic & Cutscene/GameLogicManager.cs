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

    [Header("Background Music")]
    public AudioSource backgroundMusicSource; 
    public AudioClip backgroundMusicClip; 

    [Header("Player Reference")]
    public PlayerManager playerManager;

    private void Start() {
        // Initializes the first round
        roundsSurvived = 0;
        amountOfKillsThisRound = 0;
        totalKills = 0;
        UpdatePlayerUI();
        NextRound(roundsSurvived);

        // Configure and start playing background music
        backgroundMusicSource.clip = backgroundMusicClip;
        backgroundMusicSource.loop = true; 
        backgroundMusicSource.Play();
    }

    private void Update() {
        // If there are no enemies in scene, player advances to next round
        if (enemiesAliveCurrently == 0) {
            roundsSurvived++;
            amountOfKillsThisRound = 0; // Needs to reset this kill counter 
            UpdatePlayerUI();
            NextRound(roundsSurvived);
        }

        // Stop music if the player is dead
        if (playerManager.isDead) {
            backgroundMusicSource.Stop();
        }
    }

    public void NextRound(int roundsSurvived) {
        for (var x = 0; x < roundsSurvived; x++) {
            // Take a random spawn point from the different mob spawners
            GameObject spawnPoint = mobSpawnPoints[Random.Range(0, mobSpawnPoints.Length)];
            
            // Instantiates an enemy (zombie) on the position of the mob spawner
            // Get the ZombieHealthManager script from this cloned zombie
            GameObject spawnedZombie = Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
            spawnedZombie.GetComponent<ZombieHealthManager>().gameLogicManager = this;
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
        }
    }
}


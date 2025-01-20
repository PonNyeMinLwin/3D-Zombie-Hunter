using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIManager : MonoBehaviour
{
    // This will control all Canvas displays for the player
    // This includes health, ammo count, rounds survived, and enemies left/killed 
    // All UI related settings for chests and main menu will be coded elsewhere 

    // Crosshair done
    
    [Header("Crosshair")]
    public GameObject crosshair;

    [Header("Ammo Display")]
    public TextMeshProUGUI gunAmmoCountText;
    public TextMeshProUGUI reloadAmmoCountText;

    [Header("Rounds Survived Display")]
    public TextMeshProUGUI roundsSurvivedCountText;

    [Header("Kills Counter Display")]
    public TextMeshProUGUI currentKillsCountText;
    public TextMeshProUGUI zombiesLeftInRoundCountText;
}

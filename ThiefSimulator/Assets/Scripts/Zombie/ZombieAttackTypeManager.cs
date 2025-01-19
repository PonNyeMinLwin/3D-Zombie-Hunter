using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Zombie/Actions/Zombie Attack Types")]
public class ZombieAttackTypeManager : ScriptableObject
{
    [Header("Attack Type Animation")]
    public string attackAnimation;

    [Header("Attack Cooldown")]
    // Prevents only one attack from happening - temporary 
    public float attackCoolDownTimer = 5f;

    [Header("Attack Distance and Direction")]
    // AttackDirection references what specific attack animation will play based zombie's direction from player
    // e.g. Left, Right Swipe Attack
    // AttackDistance references the distance necessary to perform a specific attack animation 
    public float maxAttackDirection = 20f;
    public float minAttackDirection = -20f; 
    public float maxAttackDistance = 1f;
    public float minAttackDistance = 2f;
}

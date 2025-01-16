using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates an asset in-game 
[CreateAssetMenu(menuName = "Items/Weapon Item")]
public class WeaponItems : Item
{
    [Header("Weapon Animations")]
    // Weapons will have different animations so we need to override the Joe (or player) Animator 
    public AnimatorOverrideController weaponAnimator;
}

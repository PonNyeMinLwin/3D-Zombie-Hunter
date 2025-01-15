using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItems : Item
{
    [Header("Weapon Animations")]
    // Weapons will have different animations so we need to override the Joe (or player) Animator 
    public AnimatorOverrideController weaponAnimator;
}

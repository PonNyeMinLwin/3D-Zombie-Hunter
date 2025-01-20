using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Ammo Box")]
public class AmmoBox : Item
{
    public int currentAmmoInBox = 60;
    public int maxAmmoBoxCapacity = 60;
}

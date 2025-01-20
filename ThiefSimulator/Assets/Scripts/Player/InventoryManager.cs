using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Temporary class
    public AmmoBox currentAmmoInReloadableInventory;

    public void UpdateSpareAmmoUI() {
        if (currentAmmoInReloadableInventory != null) {
            FindObjectOfType<PlayerUIManager>().reloadAmmoCountText.text = currentAmmoInReloadableInventory.currentAmmoInBox.ToString();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    [SerializeField] private int ammoInChest = 60;
    public string InteractionPrompt => prompt; 
    
    public bool Interact(Interactor interactor) {
        Debug.Log("Chest opened - refilling ammo");

        if (interactor.TryGetComponent<PlayerManager>(out var playerManager))
        {
            int spareAmmoCapacity = playerManager.inventoryManager.currentAmmoInReloadableInventory.maxAmmoBoxCapacity - playerManager.inventoryManager.currentAmmoInReloadableInventory.currentAmmoInBox;
            int ammoToTransfer = Mathf.Min(ammoInChest, spareAmmoCapacity);

            playerManager.inventoryManager.currentAmmoInReloadableInventory.currentAmmoInBox += ammoToTransfer;
            ammoInChest -= ammoToTransfer;

            // Update the UI
            playerManager.inventoryManager.UpdateSpareAmmoUI();
            Debug.Log($"Player took {ammoToTransfer} ammo from chest. Ammo left in chest: {ammoInChest}");
            return true;
        }

        return true;
    }
}

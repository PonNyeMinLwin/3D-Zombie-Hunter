using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactPoint;
    [SerializeField] private float interactPointArea = 0.5f;
    [SerializeField] private LayerMask interactableMask;    
    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int collidersFound;

    private void Update() {
        // Will check if any collider overlaps are found 
        collidersFound = Physics.OverlapSphereNonAlloc(interactPoint.position, interactPointArea, colliders, interactableMask);

        // When player finds interactables, this function will find all classes that have IInteractable 
        if (collidersFound > 0) {
            var interactable = colliders[0].GetComponent<IInteractable>();

            // If there is an interactable and the player presses E, they will interact with the object
            if (interactable != null && Keyboard.current.eKey.wasPressedThisFrame) {
                interactable.Interact(this);
            }
        }
    }

    //Testing
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactPoint.position, interactPointArea);
    }
}

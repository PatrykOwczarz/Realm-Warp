using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : Interactable
{
    public GameObject doorToOpen;

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up " + powerup.name);

        doorToOpen.SetActive(false);

        Destroy(gameObject);
    }

}

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

    // a version of the pickup code which is used to open doors in the level.
    // the door is open by disabling the gameobject defined by the doorToOpen variable above.
    // that game object is assigned in the inspector in Unity.
    void PickUp()
    {
        Debug.Log("Picking up " + powerup.name);

        doorToOpen.SetActive(false);

        Destroy(gameObject);
    }

}

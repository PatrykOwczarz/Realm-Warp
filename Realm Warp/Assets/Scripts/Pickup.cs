using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Parts of this script was based on this video guide:
// https://www.youtube.com/watch?v=9tePzyL6dgc
// and the powerup implementation was based on the following guide:
// https://www.youtube.com/watch?v=HQNl3Ff2Lpo
public class Pickup : Interactable
{
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up " + powerup.name);

        // Based on the name of the powerup, performs different functions and the object is destroyed.
        if (powerup.name == "Dark Realm Orb")
        {
            GameInformation.instance.SetRealmWarp(true);
        }
        else if (powerup.name == "Health Orb")
        {
            GameInformation.instance.GetPlayer().GetComponent<Player>().Heal((int)powerup.amount);
        }
        else if (powerup.name == "Mana Orb")
        {
            GameInformation.instance.GetPlayer().GetComponent<Player>().RegainMana((int)powerup.amount);
        }
        Destroy(gameObject);
    }

}

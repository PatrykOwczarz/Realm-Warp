using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinishPickup : Interactable
{
    public GameObject menuToOpen;

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    // picking up the finish orb will toggle the level completed menu and pause the game.
    void PickUp()
    {
        Debug.Log("Picking up " + powerup.name);

        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        menuToOpen.SetActive(true);

        Destroy(gameObject);
    }
}

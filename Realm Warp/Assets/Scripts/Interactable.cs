using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used knowledge from this guide: https://www.youtube.com/watch?v=9tePzyL6dgc
public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    GameObject player;

    bool hasInteracted = false;

    public virtual void Interact()
    {
        // This method is meant to be overwritten
        Debug.Log("Interacting with " + transform.name);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameInformation.instance.GetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        // If we are close enough
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= radius)
        {
            // Interact with the object
            Interact();
            hasInteracted = true;
        }
    }

    // Draws a wire frame in the inspector, used for debugging.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);

    }
}

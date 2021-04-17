using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used knowledge from this guide: https://www.youtube.com/watch?v=9tePzyL6dgc
public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    GameObject player;
    new MeshRenderer renderer;

    public Powerup powerup;

    float initialY;
    bool moveDown;

    public virtual void Interact()
    {
        // This method is meant to be overwritten
        Debug.Log("Interacting with " + transform.name);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameInformation.instance.GetPlayer();
        initialY = transform.position.y;
        moveDown = false;
        renderer = GetComponent<MeshRenderer>();
        ChangeMaterial(powerup.material);
    }

    // Update is called once per frame
    void Update()
    {
        // This makes the object move up and down based the initial y position of the object.
        if (moveDown)
        {
            transform.position -= Vector3.up * Time.deltaTime;
        }
        else if (!moveDown)
        {
            transform.position += Vector3.up * Time.deltaTime;
        }

        if (transform.position.y >= (initialY + 0.5f))
        {
            moveDown = true;    
        }
        else if (transform.position.y < initialY)
        {
            moveDown = false;
        }

        // If we are close enough
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= radius)
        {
            // Interact with the object
            Interact();
        }
    }

    // Draws a wire frame in the inspector, used for debugging.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);

    }

    // Simple function that sets the material to the one defined in the powerup Scriptable object.
    protected void ChangeMaterial(Material material)
    {
        renderer.material = material;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script was based on the following guide:
// https://www.youtube.com/watch?v=onpteKMsE84
// Only the implementation of the raycast is reused, everything else is my own implementation of the telekinesis system unless otherwise stated in the TelekinesisController script.

public class RaycastTelekinesis : MonoBehaviour
{
   
    public bool isShooting = false;
    public Transform raycastOrigin;
    public Transform raycastDesitnation;
    private TelekinesisController controller;

    Ray ray;
    RaycastHit hitInfo;

    void Start()
    {
        controller = GetComponent<TelekinesisController>();
    }

    // shoots a raycast at the crosshair position from the hand.
    // if the ray collides with a game object with the tag "Telekinesis" the object is passed to the TelekinesisController.
    public void ShootRay()
    {
        isShooting = true;

        ray.origin = raycastOrigin.position;
        ray.direction = raycastDesitnation.position - raycastOrigin.position;
        if(Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.CompareTag("Telekinesis"))
            {
                controller.SetTarget(hitInfo.rigidbody);
            }
            
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1.0f);
        }
    }
    
    // (May be not needed)
    // Sets is shooting to false;
    public void StopRay()
    {
        isShooting = false;
    }

    // fetches and returns the state of isReady from the telekinesis controller.
    public bool GetIsReady()
    {
        return controller.GetIsReady();
    }

    // returns the target tag from the telekinesis controller.
    public string GetTargetTag()
    {
        return controller.GetTargetTag();
    }
}

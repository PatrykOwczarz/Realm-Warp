using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script was based on the following guide:
// https://www.youtube.com/watch?v=onpteKMsE84
// This script positions a game object at the point of collision of the crosshair.
// This allows for more accurate targeting rather than using the forward vector of the camera.
public class CrosshairTarget : MonoBehaviour
{
    Camera mainCamera;
    Ray ray;
    RaycastHit hitInfo;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // simple implementation that shoots a ray in a direction and sets the object that this script is attached to, to the destination.
        // this helps with making sure that the direction in which the telekinetic object is thrown is consitent with the crosshair.
        ray.origin = mainCamera.transform.position;
        ray.direction = mainCamera.transform.forward;
        Physics.Raycast(ray, out hitInfo);
        transform.position = hitInfo.point;
    }
}

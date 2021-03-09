using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTelekinesis : MonoBehaviour
{
   
    public bool isShooting = false;
    public Transform raycastOrigin;
    public Transform raycastDesitnation;

    Ray ray;
    RaycastHit hitInfo;
    public void ShootRay()
    {
        isShooting = true;

        ray.origin = raycastOrigin.position;
        ray.direction = raycastDesitnation.position - raycastOrigin.position;
        if(Physics.Raycast(ray, out hitInfo))
        {
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1.0f);
        }
    }

    public void StopRay()
    {
        isShooting = false;
    }

}

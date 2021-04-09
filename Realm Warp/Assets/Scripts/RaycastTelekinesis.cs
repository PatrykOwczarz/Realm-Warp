using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void StopRay()
    {
        isShooting = false;
    }

    public bool GetIsReady()
    {
        return controller.GetIsReady();
    }

    public string GetTargetTag()
    {
        return controller.GetTargetTag();
    }
}

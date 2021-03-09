﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAiming : MonoBehaviour
{

    public float turnSpeed = 15;
    public float aimDuration = 0.3f;
    public Rig aimLayer;

    Camera mainCamera;
    RaycastTelekinesis telekinesisHand;
    

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        telekinesisHand = GetComponentInChildren<RaycastTelekinesis>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
    }

    private void LateUpdate()
    {
        if (aimLayer)
        {
            if (Input.GetButton("Fire2"))
            {
                aimLayer.weight += Time.deltaTime / aimDuration;

            }
            else
            {
                aimLayer.weight -= Time.deltaTime / aimDuration;
            }
        }

        // modify to only shoot when aiming.
        if (Input.GetButtonDown("Fire1"))
        {
            telekinesisHand.ShootRay();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            telekinesisHand.StopRay();
        }
    }
}

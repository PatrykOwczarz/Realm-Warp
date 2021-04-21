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
    public ForcePush forcePush;
    bool canForcePush = true;

    Player player;
    CharacterController cc;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        telekinesisHand = GetComponentInChildren<RaycastTelekinesis>();
        player = GetComponent<Player>();
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
    }

    private void LateUpdate()
    {
        // aimLayer is the rig layer controlling the arm movement of aiming.
        if (aimLayer)
        {
            // The player has to aim up to shoot a raycast to see if the player hits a telekinesis object.
            if (Input.GetButton("Fire2"))
            {
                // moves hand into position based on time
                aimLayer.weight += Time.deltaTime / aimDuration;

                // the player shoots a ray at a target and if the target is a telekinesis target, reduce mana by 30.
                if (Input.GetButtonDown("Fire1") && telekinesisHand.GetIsReady())
                {
                    if (player.currentMana >= 30)
                    {
                        telekinesisHand.ShootRay();
                        if(telekinesisHand.GetTargetTag() == "Telekinesis")
                        {
                            player.UseMana(30);
                        }
                    }
                }
                // stops the ray if the shoot key is released
                if (Input.GetButtonUp("Fire1") && telekinesisHand.GetIsReady())
                {
                    telekinesisHand.StopRay();
                }

            }
            else
            {
                // moves aiming hand down based on time
                aimLayer.weight -= Time.deltaTime / aimDuration;
            }

            // if realm warp is active, allow the player to press Q to force push.
            if (GameInformation.instance.GetRealmWarp())
            {
                // Refactor this code, currently the hitbox for push back is on for as long as you hold the key down. It should appear and disappear quickly even if key is held down.
                // --------------------------------------------------------------------------------------------------------------------------------------------------------------------
                if (Input.GetKeyDown(KeyCode.Q) && cc.isGrounded)
                {
                    if (player.currentMana >= 20 && canForcePush)
                    {
                        player.UseMana(20);
                        animator.SetTrigger("Force");
                        forcePush.gameObject.SetActive(true);
                        canForcePush = false;
                    }

                }        
                // deactivates the force push functionality when the player releases the Q key.
                if (Input.GetKeyUp(KeyCode.Q))
                {
                    forcePush.gameObject.SetActive(false);
                    canForcePush = true;    
                }
            }
            else
            {
                // making sure that the collider is turned off when not in realm warp.
                forcePush.gameObject.SetActive(false);
                canForcePush = true;
            }
        }
    }
}

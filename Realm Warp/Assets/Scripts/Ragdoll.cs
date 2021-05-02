﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is based on the following guide:
// https://www.youtube.com/watch?v=oLT4k-lrnwg
// I have created my own collision implementation. Did not follow the hitbox implementation. Only the creation of a ragdoll.
public class Ragdoll : MonoBehaviour
{
    Rigidbody[] rigidBodies;
    Animator animator;
    public Rigidbody hip;
    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
        DeactivateRagdoll();

    }

    // for each limb, deactivates the ragdoll by deactivating its motion in the physics agent.
    public void DeactivateRagdoll()
    {
        foreach (var rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = true;
        }
        isDead = false;
        animator.enabled = true;
    }

    // for each limb, activates the ragdoll by allowing motion of each collider on the ragdoll.
    public void ActivateRagdoll()
    {
        foreach (var rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = false;
            // This allows the player to pickup the ragdoll object.
            //rigidBody.gameObject.tag = "Liftable";
        }
        // The first initial collision adds score for defeating the enemy.
        if (!isDead)
        {
            GameInformation.instance.SetScore(GameInformation.instance.GetScore() + 10);
            GameInformation.instance.SetEnemyCount(GameInformation.instance.GetEnemyCount() - 1);
            Debug.Log(GameInformation.instance.GetEnemyCount());
            Destroy(gameObject, 5);
        }
        isDead = true;
        animator.enabled = false;
    }

    // a method that mekes applying forces to the enemy AI simple.
    public void ApplyForce(Vector3 force)
    {
        
        hip.AddForce(force, ForceMode.VelocityChange);

    }

    // checks if the target is dead or not. Applied in the enemy AI scripts.
    public bool GetIsDead()
    {
        return isDead;
    }

}

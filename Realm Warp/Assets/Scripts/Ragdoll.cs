using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

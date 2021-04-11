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

    public void DeactivateRagdoll()
    {
        foreach (var rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = true;
        }
        isDead = false;
        animator.enabled = true;
    }

    public void ActivateRagdoll()
    {
        foreach (var rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = false;
        }
        isDead = true;
        animator.enabled = false;
    }

    public void ApplyForce(Vector3 force)
    {
        
        hip.AddForce(force * hip.mass, ForceMode.Impulse);

    }

    public bool GetIsDead()
    {
        return isDead;
    }
}

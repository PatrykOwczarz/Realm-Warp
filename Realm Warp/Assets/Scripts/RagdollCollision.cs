using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RagdollCollision : MonoBehaviour
{
    Ragdoll ragdoll;
    NavMeshAgent agent;

    public void InitialiseValues(NavMeshAgent agent, Ragdoll ragdoll)
    {
        this.agent = agent;
        this.ragdoll = ragdoll;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // tracks the ragdoll collision on each limb of the ragdoll and apply's a force when the object collides with the telekinesis object.
        // this implementation may need to be tweaked as based on the way the object collides with each limb, it may cause weird effects on collision.
        // this is something to consider in further tweaking and polishing. The effect created is consistent with what I wanted to create.
        if (collision.gameObject.CompareTag("Telekinesis"))
        {
            agent.isStopped = true;
            ragdoll.ActivateRagdoll();
            ragdoll.ApplyForce(collision.rigidbody.velocity);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform playerTransform;
    Animator animator;
    Ragdoll ragdoll;
    CapsuleCollider hitCollider;

    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;
    float timer = 0.0f;

    bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        ragdoll = GetComponent<Ragdoll>();
        hitCollider = GetComponent<CapsuleCollider>();

        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            float sqDistance = (playerTransform.position - agent.destination).sqrMagnitude;
            if (sqDistance > maxDistance*maxDistance)
            {
                if (!isDead)
                {
                    agent.destination = playerTransform.position;
                }
            }
            timer = maxTime;
        }
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // currently makes use of the capsule collider on top of the model. Could rework this model to detect collision on each limb joint.
        // this would make collision more realistic and consistent.
        if (collision.gameObject.CompareTag("Telekinesis"))
        {
            agent.isStopped = true;
            isDead = true;
            hitCollider.enabled = false;
            ragdoll.ActivateRagdoll();
            ragdoll.ApplyForce(collision.rigidbody.velocity);
        }
    }
}

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

    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;
    float timer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        ragdoll = GetComponent<Ragdoll>();

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidBody in rigidBodies)
        {
            rigidBody.gameObject.AddComponent<RagdollCollision>();
            rigidBody.GetComponent<RagdollCollision>().InitialiseValues(agent, ragdoll);
        }
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    public void RunAtPlayer()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        { 
            float sqDistance = (playerTransform.position - agent.destination).sqrMagnitude;
            if (sqDistance > maxDistance * maxDistance)
            {
                if (!ragdoll.GetIsDead())
                {
                    agent.stoppingDistance = 2f;
                    agent.destination = playerTransform.position;
                }
            }
            timer = maxTime;
        }
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    public void MoveToLocation(Transform waypoint)
    {
        agent.stoppingDistance = 1f;
        agent.destination = waypoint.position;
    }

    public void LookAtPlayer()
    {
        if (!ragdoll.GetIsDead())
        {
            agent.velocity = Vector3.zero;
            var relativePos = playerTransform.position - transform.position;
            relativePos = new Vector3(relativePos.x, 0, relativePos.z);
            var rotation = Quaternion.LookRotation(relativePos);
            transform.rotation = rotation;
        }
    }

}

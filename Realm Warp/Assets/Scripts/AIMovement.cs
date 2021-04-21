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

    public float maxTime = 0.5f;
    public float maxDistance = 1.0f;
    float timer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        ragdoll = GetComponent<Ragdoll>();

        // Adds collision for each limb of the ragdoll and initialises the values for collision to work.
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
        // sets animation based on NavMeshAgent velocity.
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    // if the agent is not dead run at the player location. Used in the AIStateMachine implmentation.
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

    // Move to a defined location. Used in the AI state machine implementation.
    public void MoveToLocation(Transform waypoint)
    {
        agent.stoppingDistance = 1f;
        agent.destination = waypoint.position;
    }

    // rotate the agent to look at the player position if they are not dead.
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

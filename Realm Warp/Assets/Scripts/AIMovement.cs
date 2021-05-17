using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Parts of this script are based on the following guide:
// https://www.youtube.com/watch?v=TpQbqRNCgM0
// This acted as the base for my movement implementation.
// The video guide creates a simple movement AI using the NavMeshAgent that just follows the player.
// I have expanded upon this implementation.

public class AIMovement : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform playerTransform;
    Animator animator;
    Ragdoll ragdoll;

    public float maxTime = 1f;
    public float maxDistance = 1.0f;
    float timer = 0.0f;

    private float defaultSpeed = 4.601099f; // this is the speed at which the animation is created at.
    public float patrolSpeed = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameInformation.instance.GetPlayer().transform;
        animator = GetComponent<Animator>();
        ragdoll = GetComponent<Ragdoll>();

        agent.speed = defaultSpeed;

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
    // this function was taken from the guide referenced at the beginning of the class.
    public void RunAtPlayer()
    {
        timer -= Time.deltaTime;
        agent.speed = defaultSpeed;
        if (timer < 0.0f)
        { 
            float sqDistance = (playerTransform.position - agent.destination).sqrMagnitude;
            if (sqDistance > maxDistance * maxDistance)
            {
                if (!ragdoll.GetIsDead())
                {
                    agent.stoppingDistance = 1.5f;
                    agent.destination = playerTransform.position;
                }
            }
            timer = maxTime;
        }
    }

    // Move to a defined location. Used in the AI state machine implementation.
    public void MoveToLocation(Transform waypoint)
    {
        agent.speed = patrolSpeed;
        agent.stoppingDistance = 1f;
        agent.destination = waypoint.position;
    }

    // rotate the agent to look at the player position if they are not dead.
    // reference: https://forum.unity.com/threads/how-to-get-an-ai-to-look-at-player-without-navmesh.289911/
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

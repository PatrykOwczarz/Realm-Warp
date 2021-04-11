using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachineMelee : MonoBehaviour
{
    public enum AIStates
    {
        PATROL,
        LOOKAT,
        FOLLOW
    }

    public AIStates currentState;
    private AIMovement movementController;
    public Transform[] Waypoints;
    public Transform player;

    public float maxTime = 4.0f;
    public float lookAtDistance = 10f;
    public float aggroDistance = 5f;
    private float timer = 0.0f;

    private int currentWaypoint;

    // Start is called before the first frame update
    void Start()
    {
        currentWaypoint = 0;
        movementController = GetComponent<AIMovement>();
        currentState = AIStates.PATROL;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case (AIStates.PATROL):
                AIPatrol();
                break;

            case (AIStates.LOOKAT):
                LookatPlayer();
                break;

            case (AIStates.FOLLOW):
                FollowPlayer();
                break;
        }
    }

    private void AIPatrol()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f) 
        {
            var AIPosition = new Vector3(this.transform.position.x, 0, this.transform.position.z);
            var distance = (Waypoints[currentWaypoint].position - AIPosition).magnitude;
            if (distance > 1f)
            {
                movementController.MoveToLocation(Waypoints[currentWaypoint]);
            }
            else if (distance < 1f)
            {
                currentWaypoint = (currentWaypoint + 1) % Waypoints.Length;
                movementController.MoveToLocation(Waypoints[currentWaypoint]);
            }
            timer = maxTime;
        }

        var distanceFromPlayer = (player.position - transform.position).magnitude;
        if (distanceFromPlayer < lookAtDistance)
        {
            currentState = AIStates.LOOKAT;
        }
    }
    
    private void LookatPlayer()
    {
        movementController.LookAtPlayer();
        var distanceFromPlayer = (player.position - transform.position).magnitude;
        if (distanceFromPlayer > lookAtDistance)
        {
            currentState = AIStates.PATROL;
        }
        else if (distanceFromPlayer < aggroDistance)
        {
            currentState = AIStates.FOLLOW;
        }
    }

    private void FollowPlayer()
    {
        movementController.RunAtPlayer();
        var distanceFromPlayer = (player.position - transform.position).magnitude;
        if (distanceFromPlayer > aggroDistance)
        {
            currentState = AIStates.LOOKAT;
        }
    }

}

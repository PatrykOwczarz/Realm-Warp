using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelekinesisController : MonoBehaviour
{
    public Transform raycastDesitnation;

    private bool isReady;

    private Rigidbody target;
    private float targetLift;
    private bool hasLiftAmount;

    public GameObject pullPosition;
    private Vector3 pullForce;
    public float pullForceModifier;
    public float maxVelocity;
    public float posistionDistanceThreshold;
    public float velocityDistanceThreshhold;

    private Vector3 throwDirection;
    

    // Enum controlling telekinesis steps
    public enum TelekinesisSteps
    {
        WAITING,
        LIFT,
        PULL,
        THROW
    }

    public static TelekinesisSteps currentStep;

    void Start()
    {
        target = null;
        hasLiftAmount = false;
        isReady = true;
        currentStep = TelekinesisSteps.WAITING;
    }

    void Update()
    {
        // if the current step is pull, throw the object when pressing left mouse button.
        if (currentStep == TelekinesisSteps.PULL)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                currentStep = TelekinesisSteps.THROW;
            }
        }
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            // a switch case to go through the stages of telekinesis.
            switch (currentStep)
            {
                case (TelekinesisSteps.WAITING):
                    // Liftable is for lifting ragdolls but force on throw is too small.
                    if (target.CompareTag("Telekinesis") || target.CompareTag("Liftable"))
                    {
                        isReady = false;
                        hasLiftAmount = false;
                        currentStep = TelekinesisSteps.LIFT;
                    }
                    break;

                case (TelekinesisSteps.LIFT):
                    LiftTarget();
                    break;

                case (TelekinesisSteps.PULL):
                    StartCoroutine(PullHoldTarget());
                    break;

                case (TelekinesisSteps.THROW):
                    target.constraints = RigidbodyConstraints.None;
                    target.GetComponentInParent<Transform>().parent = null;
                    ThrowTarget();
                    isReady = true;
                    break;
            }

        }
        
    }

    // Setting a target for the telekinesis controller to detect. If the target has a tag "Telekinesis" the controller recognises it.
    public void SetTarget(Rigidbody tar)
    {
        this.target = tar;
    }

    // Returns the name of the target tag.
    public string GetTargetTag()
    {
        if (target != null)
        {
            return target.tag;
        }
        else
        {
            return "null";
        }
            
    }

    // return is ready.
    public bool GetIsReady()
    {
        return isReady;
    }

    // function to lift the telekinesis object from the ground into the air.
    private void LiftTarget()
    {
        // create a target lift position
        if (!hasLiftAmount)
        {
            targetLift = target.position.y + 1f;
            hasLiftAmount = true;
        }

        // apply a force upwards until the target reaches the above defined position. When position is met, the pull step is initiated.
        if (target.position.y < targetLift)
        {
            target.velocity = Vector3.zero;
            target.AddForce(Vector3.up * target.mass * 2f, ForceMode.Impulse);
        }
        else
            currentStep = TelekinesisSteps.PULL;
        
    }

    // 
    IEnumerator PullHoldTarget()
    {
        while (true)
        {
            float distanceToPullPosition = Vector3.Distance(target.position, pullPosition.transform.position);

            // if the distance to the desired position is less than the threshold, the game object becomes attached as a child of the player, hence following his movement.
            // the position of the rigidbody is also frozen so that the object would not move around due to gravity or other forces in the physics engine.
            // it will however keep its rotational velocity which will make it rotate around in the players hand.
            if (distanceToPullPosition < posistionDistanceThreshold)
            {
                target.position = pullPosition.transform.position;
                target.constraints = RigidbodyConstraints.FreezePosition;
                target.GetComponentInParent<Transform>().parent = pullPosition.transform;
                break;
            }

            Vector3 pullDirection = pullPosition.transform.position - target.position;

            pullForce = pullDirection.normalized * pullForceModifier;

            // if the target is not at the position threshold apply a force in the direction of the pull position.
            if (target.velocity.magnitude < maxVelocity && distanceToPullPosition > velocityDistanceThreshhold)
            {
                target.AddForce(pullForce, ForceMode.Force);
            }
            // if the velocity of the object would exceed the max velocity, set the velocity to the max speed.
            else
            {
                target.velocity = pullDirection.normalized * maxVelocity;
            }

            yield break;

        }

    }

    private void ThrowTarget()
    {
        //retrieve crosshair aim and throw the object at the crosshair location.
        throwDirection = raycastDesitnation.position - target.position;

        // if the crosshair is not aiming at a wall, use the camera forward vector instead. As if the raycast does not hit anything, it returns (0,0,0) as the position.
        if (raycastDesitnation.position == new Vector3(0f,0f,0f))
        {
            throwDirection = Camera.main.transform.forward;
        }

        target.AddForce(throwDirection.normalized * (50f * target.mass), ForceMode.Impulse);
        currentStep = TelekinesisSteps.WAITING;
        target = null;

    }
}

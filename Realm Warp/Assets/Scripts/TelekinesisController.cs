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
    private Vector3 pullDirection;

    private Vector3 throwDirection;
    

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
                    if (target.CompareTag("Telekinesis"))
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
                    PullHoldTarget();
                    break;

                case (TelekinesisSteps.THROW):
                    ThrowTarget();
                    isReady = true;
                    break;
            }

        }
        
    }

    public void SetTarget(Rigidbody tar)
    {
        this.target = tar;
    }

    public bool GetIsReady()
    {
        return isReady;
    }

    //function to lift the telekinesis object from the ground into the air.
    private void LiftTarget()
    {
        if (!hasLiftAmount)
        {
            targetLift = target.position.y + 1f;
            hasLiftAmount = true;
        }

        if (target.position.y < targetLift)
        {
            target.velocity = Vector3.zero;
            target.AddForce(Vector3.up * target.mass * 2f, ForceMode.Impulse);
        }
        else
            currentStep = TelekinesisSteps.PULL;
        
    }

    private void PullHoldTarget()
    {
        pullDirection = pullPosition.transform.position - target.position;

        //checks if levitation object collides and stops the motion if it does. Kinda clunky need to look at a better implementation.
        if (Physics.CheckSphere(pullPosition.transform.position , 0.01f))
        {
            target.velocity = Vector3.zero;
            target.AddForce(Vector3.up * 9.8f, ForceMode.Acceleration);

        }
        //if the target object does not collide it keeps on moving the object to desired location.
        else
        {
            target.AddForce(pullDirection.normalized * target.mass, ForceMode.Impulse);

        }


    }

    private void ThrowTarget()
    {
        //retrieve crosshair aim and throw the object at the crosshair location.
        throwDirection = raycastDesitnation.position - target.position;
        target.AddForce(throwDirection * target.mass * 4f, ForceMode.Impulse);
        currentStep = TelekinesisSteps.WAITING;
        target = null; 
    }
}

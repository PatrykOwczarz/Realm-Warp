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
    public float pullForceModifier = 1.0f;
    public float maxVelocity;
    public float posistionDistanceThreshold;
    public float velocityDistanceThreshhold;

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

    IEnumerator PullHoldTarget()
    {
        while (true)
        {
            float distanceToPullPosition = Vector3.Distance(target.position, pullPosition.transform.position);

            if (distanceToPullPosition < posistionDistanceThreshold)
            {
                target.position = pullPosition.transform.position;
                target.constraints = RigidbodyConstraints.FreezePosition;
                target.GetComponentInParent<Transform>().parent = pullPosition.transform;
                break;
            }

            Vector3 pullDirection = pullPosition.transform.position - target.position;

            pullForce = pullDirection.normalized * pullForceModifier;

            if (target.velocity.magnitude < maxVelocity && distanceToPullPosition > velocityDistanceThreshhold)
            {
                target.AddForce(pullForce, ForceMode.Force);
            }
            //if the target object does not collide it keeps on moving the object to desired location.
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
        target.AddForce(throwDirection.normalized * (50f * target.mass), ForceMode.Impulse);
        currentStep = TelekinesisSteps.WAITING;
        target = null; 
    }
}

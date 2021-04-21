using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    public float jumpHeight;
    public float gravity;
    public float stepDown;
    public float airControl;
    public float jumpDamp;
    public float groundSpeed;
    public float pushPower;

    Animator animator;
    CharacterController cc;
    Vector2 input;

    Vector3 rootmotion;
    Vector3 velocity;
    bool isJumping;

    bool isFloating = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // input based on the unity defined horizontal and vertical values ( A, D for horizontal and W, S for vertical)
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        
        // feed the input values into the animator which moves the character based on the root motion applied to the animation.
        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);

        if (Input.GetKeyDown(KeyCode.Space)){
            Jump();
        }

        // If the player enters the dark realm, they can toggle floating by pressing the E key. The player floats in the current y plane. 
        if (GameInformation.instance.GetRealmWarp())
        {
            if (Input.GetKeyDown(KeyCode.E) && !cc.isGrounded)
            {
                isFloating = !isFloating;
                if (isFloating)
                {
                    gravity = 0f;
                    airControl = 5f;
                    velocity = Vector3.zero;
                }
                else
                {
                    gravity = 20f;
                    airControl = 2.5f;
                }

            }
        }
        else
        {
            gravity = 20f;
            airControl = 2.5f;
        }
    }

    // https://www.youtube.com/watch?v=4y4QXEPnkgY at 6:30
    private void OnAnimatorMove()
    {
        rootmotion += animator.deltaPosition;
    }

    private void FixedUpdate()
    {
        if (isJumping) //In air
        {
            UpdateInAir();
        }
        else //Grounded
        {
            UpdateOnGround();
        }

    }

    // updates the position on ground using the character controller by multiplying the root motion speed of the animation with the groundSpeed variable.
    private void UpdateOnGround()
    {
        Vector3 stepForwardAmount = rootmotion * groundSpeed;
        // controls the speed at which the character steps down if possible.
        Vector3 stepDownAmmount = Vector3.down * stepDown;
        
        cc.Move(stepForwardAmount + stepDownAmmount);
        rootmotion = Vector3.zero;

        if (!cc.isGrounded)
        {
            SetInAir(0);
        }
    }

    // reduces the y velocity by the gravity variable and calculates the displacement in air.
    private void UpdateInAir()
    {
        velocity.y -= gravity * Time.deltaTime;
        Vector3 displacement = velocity * Time.fixedDeltaTime;
        displacement += CalculateAirControl();
        cc.Move(displacement);
        isJumping = !cc.isGrounded;
        rootmotion = Vector3.zero;
        animator.SetBool("isJumping", isJumping);
    }

    // calculates a fraction of movement control in air.
    Vector3 CalculateAirControl()
    {
        return ((transform.forward * input.y) + (transform.right * input.x)) * (airControl / 100);
    }

    // sets the jump velocity of the player.
    void Jump()
    {
        if (!isJumping)
        {
            float jumpVelocity = Mathf.Sqrt(2 * gravity * jumpHeight);
            SetInAir(jumpVelocity);
        }
    }

    // adds y velocity to simulate jumping and changes the animation to jumping.
    private void SetInAir(float jumpVelocity)
    {
        isJumping = true;
        velocity = animator.velocity * jumpDamp * groundSpeed;
        velocity.y = jumpVelocity;
        animator.SetBool("isJumping", isJumping);
    }

    // Unity created function to create collision detection without the use of a rigidbody.
    // reference: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnControllerColliderHit.html
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
            return;

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3f)
            return;

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * pushPower;
    }
}

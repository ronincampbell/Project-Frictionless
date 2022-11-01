using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PlayerMovement : MonoBehaviour
{

    // Base code provided by  Dave / GameDevelopment on YouTube

    [Header("Movement")]
    public float moveSpeed;
    public float walkSpeed;
    public float slideSpeed;
    public float wallrunSpeed;
    public float swingSpeed;

    public float dashSpeed;
    public float dashSpeedChangeFactor;

    public float maxYSpeed;

    public float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;
    private MovementState lastState;
    private bool keepMomentum;

    public float groundDrag;

    [Header("Jumping")]
    public float normalJump;
    private float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public int jumpAmount = 2;
    bool readyToJump;
    private float padRef;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;
    public PlayerCam cam;

    [Header("SFX")]
    public GameObject Walksource;
    public AudioSource audioSource;
    public AudioClip bounceSFX;
    public AudioClip jumpSFX;
    public float volume = 1f;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.LeftControl; 

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;
    public bool onPad;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;


    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;




    // define current movment states
    public MovementState state;
    public enum MovementState
    {
        walking,
        wallrunning,
        swinging,
        crouching,
        sliding,
        dashing,
        air
        
    }

    public bool sliding;
    public bool wallrunning;
    public bool dashing;
    public bool swinging;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;


        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.4f, whatIsGround);

        MyInput();
        SpeedControl();
        ResetDoubleJump();
        StateHandler();
        CheckForPad();

        // handle drag
        if (state == MovementState.walking || state == MovementState.crouching || state == MovementState.sliding)
        {
            rb.drag = groundDrag;
        }
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //when to jump
        if(Input.GetKey(jumpKey) && readyToJump && jumpAmount > 1 && !wallrunning)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // when to crouch
        if(Input.GetKeyDown(crouchKey) && grounded)
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down *5f, ForceMode.Impulse);
        }

        //stop crouch
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            cam.DoFov(80);
        }
    }

    private void StateHandler()
    {
        // Mode - Dashing
        if(dashing)
        {
            state = MovementState.dashing;
            desiredMoveSpeed = dashSpeed;
            speedChangeFactor = dashSpeedChangeFactor;
            Walksource.SetActive(false);
        }

        // Mode - Swinging
        else if(swinging)
        {
            state = MovementState.swinging;
            moveSpeed = swingSpeed;
            Walksource.SetActive(false);
        }
        // Mode - Wallrunning
        else if(wallrunning)
        {
            state = MovementState.wallrunning;
            desiredMoveSpeed = wallrunSpeed;
            if (horizontalInput > 0 || verticalInput > 0)
                Walksource.SetActive(true);
            else
                Walksource.SetActive(false);

        }

        // Mode - Sliding
        else if(sliding && grounded)
        {
            state = MovementState.sliding;
            Walksource.SetActive(false);

            if(OnSlope() && rb.velocity.y < 0.1f)
                desiredMoveSpeed = slideSpeed;

            else
                desiredMoveSpeed = walkSpeed;

        } 
        // Mode - Crouching
        else if(Input.GetKey(crouchKey) && grounded)
        {
            state = MovementState.crouching;
            desiredMoveSpeed = crouchSpeed;
        }


        // Mode - Walking
        else if(grounded)
        {
            jumpAmount = 2;
            state = MovementState.walking;
            desiredMoveSpeed = walkSpeed;
            if (horizontalInput > 0 || verticalInput > 0)
                Walksource.SetActive(true);
            else
                Walksource.SetActive(false);

        }
        // Mode - Air
        else
        {
            state = MovementState.air;
            desiredMoveSpeed = walkSpeed;
            Walksource.SetActive(false);
        }

        // check if desiredMoveSpeed has changed drastically
        bool desiredMoveSpeedHasChanged = desiredMoveSpeed != lastDesiredMoveSpeed;
        if (lastState == MovementState.dashing) keepMomentum = true;

        if (desiredMoveSpeedHasChanged)
        {
            if (keepMomentum)
            {
                StopAllCoroutines();
                StartCoroutine(SmoothlyLerpMoveSpeed());
            }
            else
            {
                StopAllCoroutines();
                moveSpeed = desiredMoveSpeed;
            }
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;
        lastState = state;

        // check if desiredMoveSpeed has changed drastically
    }

    private float speedChangeFactor;
    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        // smoothly lerp movementSpeed to desired value
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        float boostFactor = speedChangeFactor;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);
            time += Time.deltaTime * boostFactor;
            yield return null;
        }

        moveSpeed = desiredMoveSpeed;
        speedChangeFactor = 1f;
        keepMomentum = false;
    }

    private void MovePlayer()
    {
        if (dashing) return;
        if (swinging) return;

        // calculate movement direction 
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on slope
        if(OnSlope())
        {
            rb.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // on ground
        else if(grounded)
            rb.AddForce(moveDirection.normalized *moveSpeed * 10f, ForceMode.Force);
        
        // in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized *moveSpeed * 10f * airMultiplier, ForceMode.Force);
        
        // turn gravity off while on slope
        if(!wallrunning) rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        // limiting speed on slopes
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // limit velocity if needed

            if(flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized *moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }

        // limit y vel
        if(maxYSpeed != 0 && rb.velocity.y > maxYSpeed)
            rb.velocity = new Vector3(rb.velocity.x, maxYSpeed, rb.velocity.z);
    }


    // jump related functions

    private void Jump()
    {
        if(!onPad)
            audioSource.PlayOneShot(jumpSFX, volume);
        exitingSlope = true;
        // reset y velocity 
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        jumpAmount = jumpAmount - 1;
        onPad = false;
    }

    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }

    private void ResetDoubleJump()
    {
        if(grounded)
            jumpAmount = 2;
    }

    public bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

    // This Allows for individual jump pads to have their own values for jump force - it is entirely my own code and i am very proud of it :)
    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (collision.gameObject.tag == "jumpPad")
        {
            onPad = true;
            sliding = false;
            padRef = other.GetComponent<JumpPadParams>().padForce;
        }
    }

    private void CheckForPad()
    {
        if(onPad == true)
        {
            audioSource.PlayOneShot(bounceSFX, volume);
            jumpForce = padRef;
            Jump();
        }
        else
            jumpForce = normalJump;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThirdPersonPlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public bool wallrunning;
    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;
    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public Transform raycastStart;
    public float raycastLength;
 
    public LayerMask whatIsGround;
    public bool grounded;

    public Transform orientation;

    float horizontal;
    float vertical;

    Vector3 verticalMoveDirection;
    Vector3 horizontalMoveDirection;


    Rigidbody rb;
    [Header("Camera")]

    
    public Transform cam;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    float angle;
    float targetAngle;
    public Vector3 direction;
    public bool lockCursor;

     [Header("Animations")]
     public Animator anim;
    public string Walkbool;
    public string Jumpbool;
    public string Idlebool;

    [Header("Sprint")]
    public Slider slider;
    public float sprintDuration = 100;

    private float sprint;

    private void Start()
    {
        if(lockCursor){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        }
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        slider.maxValue = sprintDuration;
        sprint = sprintDuration;
    }

    private void Update()
    {
        // ground check
        CheckGround();

        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
        if(Input.GetButton("Boost") && sprint > 0)
        {
            moveSpeed = 20;
            sprint -= 0.33f;
            slider.value = sprint;
        }
        else
        {
            moveSpeed = 7;
            if(sprint < sprintDuration)
            {
                sprint += 0.5f;
            slider.value = sprint;
            }
            
        }
    }

    private void CheckGround()
    {
    
        Vector3 direction = transform.TransformDirection(Vector3.down);
   
        if (Physics.Raycast(raycastStart.position, direction, out RaycastHit hit, raycastLength))
        {
            Debug.DrawRay(raycastStart.position, direction * raycastLength, Color.red);
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    private void MyInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f){
            anim.SetBool(Walkbool, true);
            float cameraAngle = cam.eulerAngles.y;
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cameraAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, gameObject.transform.rotation.z);
        } else {
            anim.SetBool(Walkbool, false);
        }
        // when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;


        if(direction.magnitude >= 0.1f){
        // on ground
        if(grounded){
            rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);

        } else if(!grounded){
            rb.AddForce(moveDir.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        }
}
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}
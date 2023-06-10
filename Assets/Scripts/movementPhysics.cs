using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class movementPhysics : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Drag")]
    //rigidbody drag
    [SerializeField] private float groundDrag = 6f;
    [SerializeField] private float airDrag = 2f;

    public float movementSpeed = 12.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 3.0f;

    //Check ground variables
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public LayerMask PlayerLayerMask;


    //Speed Variables
    private Vector3 lastMove;

    private bool isSprinting;
    private bool isCrouching;

    public float zLook;

    private float currentMovementSpeed;
    private float currentGravity;

    Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentMovementSpeed = movementSpeed;
        lastMove = Vector3.zero;
        currentGravity = gravity;

    }

    // Update is called once per frame
    void Update()
    {
        handleMovementSpeed();
        ControlDrag();
        handleMove();
        handleJump();
        handleGravity();
    }

    public void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }



    void handleMove()
    {
        // Get Movement Input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Move Character
        Vector3 move = transform.right * x + transform.forward * z;


        rb.AddForce(move.normalized * currentMovementSpeed, ForceMode.Acceleration);

    }

    public void resetYVelocity()
    {
        velocity.y = 0;
    }

    void handleGravity()
    {
        // Gravity
        if (Input.GetButton("Jump"))
        {
            currentGravity = gravity;
        }
        else
        {
            currentGravity = gravity * 2;
        }

        velocity.y = currentGravity * Time.deltaTime;
        //rb.AddForce(velocity, ForceMode.Acceleration);

    }

    void handleJump()
    {
        //Grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
        }
    }


    void handleMovementSpeed()
    {
        // Sprint
        isSprinting = Input.GetKey(KeyCode.LeftShift) ? true : false;
        currentMovementSpeed = isCrouching ? movementSpeed / 4f : isSprinting ? movementSpeed * 1.5f : movementSpeed;
    }

}

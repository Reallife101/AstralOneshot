using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour
{
    public CharacterController controller;

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
        currentMovementSpeed = movementSpeed;
        lastMove = Vector3.zero;
        currentGravity = gravity;
    
    }

    // Update is called once per frame
    void Update()
    {
        handleMovementSpeed();
        crouch();
        handleMove();
        handleJump();
        handleGravity();
    }
    


    void handleMove()
    {
        // Get Movement Input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Move Character
        Vector3 move = transform.right * x + transform.forward * z;

        if (!isGrounded)
        {
            move = lastMove + move * 0.1f;
            controller.Move(move * currentMovementSpeed * Time.deltaTime);

        }
        else
        {
            controller.Move(move * currentMovementSpeed * Time.deltaTime);
            lastMove = move;
        }
        
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
            currentGravity = gravity*2;
        }

        velocity.y += currentGravity* Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
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
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }


    void handleMovementSpeed()
    {
        // Sprint
        isSprinting = Input.GetKey(KeyCode.LeftShift) ? true : false;
        currentMovementSpeed = isCrouching ? movementSpeed/4f : isSprinting ? movementSpeed*1.5f : movementSpeed;
    }

    void crouch()
    {
        isCrouching = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.C) ? true : false;

        if (isCrouching)
        {
            controller.height = 1f;
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z);
            groundCheck.transform.position = new Vector3(transform.position.x, transform.position.y -0.4f, transform.position.z);
        }
        else
        {
            controller.height = 2f;
            groundCheck.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
        }
    }
}

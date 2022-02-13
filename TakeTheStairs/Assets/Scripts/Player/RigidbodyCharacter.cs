using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RigidbodyCharacter : MonoBehaviour
{
    public float moveSpeed = 60f;
    public float airSpeed = 10f;
    public float jumpForce = 8f;
    public float groundDrag = 8f;
    public float airDrag = 1.5f;
    
    public LayerMask groundMask;
    
    private float GroundDistance = 0.3f;
    private Vector3 moveDirection;
    private Vector3 slopeMoveDirection;
    private float horizontalMovement;
    private float verticalMovement;

    private bool isGrounded = true;

    private Transform groundCheck;
    private Rigidbody rb;
    private KeyCode jumpKey = KeyCode.Space;
    private RaycastHit slopeHit;
    private void Start()
    {
        groundCheck = transform.GetChild(0);
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, GroundDistance, groundMask, QueryTriggerInteraction.Ignore);
        MyInput();
        ControlDrag();

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
    }

    void ControlDrag()
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

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, 1.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
        }
        return false;
    }
    
    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Acceleration);
        }
        if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed, ForceMode.Acceleration);
        }
        if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * airSpeed, ForceMode.Acceleration);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}

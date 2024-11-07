using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpyMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody;

    //basic character movements this way, we put as public so we can modify if needed.
    public float moveSpeed = 6f;
    public float maxJumpHeight = 2.7f;
    public float maxJumpTime = 1f;

    //gravity and the way the character jumps so it drags him down too
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f),2);

    //these will be useful to see if the character can jump or not
    public bool grounded { get; private set; }
    public bool jumping { get; private set; }

    //added animation of my own
    public Animator animator;


    private Vector2 velocity;
    private float inputAxis;




    //start the character with rigidbody
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        //making the character move with a and d
        HorizaontalMove();

        //cheching ik collision to see grounded as true or false
        grounded = rigidbody.Raycast(Vector2.down);

        //used for the animations
        if (grounded)
        {
            animator.SetBool("IsJump", false);
            GroundedMovement();

            
        }
        
        if (!grounded)
        {
            animator.SetBool("IsJump", true);
        }

        animator.SetFloat("Speed", Mathf.Abs(velocity.x));
        
        //always have gravity to the player
        ApplyGravity();

    }

    private void HorizaontalMove()
    {
        //using the predefined horizontal given by Unity to move left and right
        inputAxis = Input.GetAxis("Horizontal");
        //moving
        velocity.x = inputAxis * moveSpeed;
    }

    //how the character moves at the ground, have to check fro gravity since if it just keep adding up to -y it will just not jump
    private void GroundedMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;


        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
            jumping = true;
            
        }
    }

    private void ApplyGravity()
    {
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f;

        velocity.y += gravity * multiplier * Time.deltaTime;


    }

    //helps with moving the player but also helpful for debuggs
    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;
        rigidbody.MovePosition(position);

        //Debug.Log("Grounded: " + grounded);

    }
    public void SetHorizontalInput(float input)
    {
        inputAxis = input;
    }






}

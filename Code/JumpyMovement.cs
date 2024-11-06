using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpyMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody;

    public float moveSpeed = 6f;
    public float maxJumpHeight = 2.7f;
    public float maxJumpTime = 1f;
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f),2);

    public bool grounded { get; private set; }
    public bool jumping { get; private set; }

    public Animator animator;


    private Vector2 velocity;
    private float inputAxis;





    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {


        HorizaontalMove();

        grounded = rigidbody.Raycast(Vector2.down);

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
        

        ApplyGravity();

    }

    private void HorizaontalMove()
    {
        //using the predefined horizontal given by Unity to move left and right
        inputAxis = Input.GetAxis("Horizontal");
        //moving
        velocity.x = inputAxis * moveSpeed;
    }

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


    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;
        rigidbody.MovePosition(position);

        //Debug.Log("Grounded: " + grounded);

    }




}

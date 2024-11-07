using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewJumpy : MonoBehaviour
{
    private new Rigidbody2D rigidbody;

    public float moveSpeed = 6f;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2);

    public bool grounded { get; private set; }
    public bool jumping { get; private set; }

    public Animator animator;

    public float testing = 0.1f;

    private Vector2 velocity;
    private float inputAxis;
    private bool jumpInput;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Only move based on the inputs set by SetInputs from DisablingControl
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
        // Moving based on the modified inputAxis provided by DisablingControl
        velocity.x = inputAxis * moveSpeed;
    }

    private void GroundedMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;

        // Only allow jumping if jumpInput is true and grounded
        if (jumpInput && grounded)
        {
            velocity.y = jumpForce;
            jumping = true;
        }
    }

    private void ApplyGravity()
    {
        bool falling = velocity.y < 0f || !jumpInput;
        float multiplier = falling ? 2f : 1f;

        velocity.y += gravity * multiplier * Time.deltaTime - testing;
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;
        rigidbody.MovePosition(position);
    }

    // This method is called by DisablingControl to set inputs
    public void SetInputs(float horizontalInput, bool jumpInput)
    {
        this.inputAxis = horizontalInput;
        this.jumpInput = jumpInput;
    }
}

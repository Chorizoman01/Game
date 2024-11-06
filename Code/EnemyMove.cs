using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private Vector2 velocity;

    public float speed = 1f;
    public Vector2 direction = Vector2.left;

    public Animator animator;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        //enabled = false;
        speed = Random.Range(1, 10);
    }

    //private void OnBecameVisible()
    //{
    //    enabled = true;
    //}

    //private void OnBecameInvisible()
    //{
    //    enabled = false;
    //}

    private void OnEnable()
    {
        if (rigidbody != null) // Check if rigidbody is assigned
        {
            rigidbody.WakeUp();
        }
    }

    //private void OnDisable()
    //{
    //    if (rigidbody != null) // Check if rigidbody is assigned
    //    {
    //        rigidbody.velocity = Vector2.zero;
    //        rigidbody.Sleep();
    //    }
    //}

    private void FixedUpdate()
    {
        velocity.x = direction.x * speed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);

        if (rigidbody.Raycast(direction))
        {
            direction = -direction;
        }

        if (rigidbody.Raycast(Vector2.down))
        {
            velocity.y = Mathf.Max(velocity.y, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            JumpyMovement jumpyMovement = collision.gameObject.GetComponent<JumpyMovement>();



            if (GameManager.Instance != null)
            {
                jumpyMovement.animator.SetBool("Death", true);
                
                GameManager.Instance.Resetlvl();
            }
            else
            {
                Debug.LogError("GameManager instance is missing!");
            }
        }
    }
}
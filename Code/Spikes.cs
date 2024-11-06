using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer

    private Vector2 velocity;
    public float speed = 5f;
    public Vector2 direction = Vector2.up;

    public AudioSource audioSource;
    public AudioClip audioclip;



    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Initially hide the spikes by disabling the SpriteRenderer
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Make the spikes visible upon player collision
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = true;
            }

            JumpyMovement jumpyMovement = collision.gameObject.GetComponent<JumpyMovement>();
            audioSource.clip = audioclip;
            audioSource.Play();


            if (GameManager.Instance != null)
            {
                

                // Optional: Add movement or other actions upon collision
                velocity.y = direction.y * speed;

                rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
                jumpyMovement.animator.SetBool("Death", true);

                GameManager.Instance.Resetlvl(0.1f);
            }
            else
            {
                Debug.LogError("GameManager instance is missing!");
            }
        }
    }
}

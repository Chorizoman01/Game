using System.Collections;
using UnityEngine;

public class EnemyReturn : MonoBehaviour
{
    private Vector2 originalPosition; // Store the original position of the enemy
    private Rigidbody2D rigidbody2D;

    public float resetDelay = 3f; // Time in seconds before resetting the position (optional)
    public float fallThresholdY = -10f; // Y position threshold; adjust as needed

    private void Awake()
    {
        // Store the enemy's starting position
        originalPosition = transform.position;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Check if the enemy's Y position has fallen below the threshold
        if (transform.position.y < fallThresholdY)
        {
            TriggerReset(); // Reset the position if it has fallen too low
        }
    }

    // Trigger the reset process immediately (without delay)
    private void TriggerReset()
    {
        // Reset velocity and gravity temporarily to stop any movement
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.gravityScale = 0f;

        // Move the enemy to the original position
        rigidbody2D.position = originalPosition;

        // Reactivate gravity after resetting position
        rigidbody2D.gravityScale = 1f; // Reset to the original gravity scale (1 by default)
    }
}

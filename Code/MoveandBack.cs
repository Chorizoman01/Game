using System.Collections;
using UnityEngine;

public class MoveandBack : MonoBehaviour
{
    private Vector2 originalPosition; // Stores the object's starting position
    public float moveDistance = 2f;   // Distance to move the object upwards
    public float delay = 5f;          // Delay time in seconds

    private void Start()
    {
        // Store the original position
        originalPosition = transform.position;

        // Start the coroutine to handle movement
        StartCoroutine(MoveUpAndReturnRoutine());
    }

    private IEnumerator MoveUpAndReturnRoutine()
    {
        while (true)
        {
            // Wait for the delay time before moving up
            yield return new WaitForSeconds(delay);

            // Move the object up by the specified distance
            transform.position = new Vector2(originalPosition.x, originalPosition.y + moveDistance);

            // Wait for the delay time before moving back
            yield return new WaitForSeconds(delay);

            // Move the object back to the original position
            transform.position = originalPosition;
        }
    }
}

using System.Collections;
using UnityEngine;

public class RandomizeMoveback : MonoBehaviour
{
    private Vector2 originalPosition; // Stores the object's starting position
    public float moveDistance = 1f;   // Distance to move the object upwards
    public float delay = 2f;          // Delay time in seconds
    public float delay2 = 2f;

    private void Start()
    {
        // Store the original position
        originalPosition = transform.position;

        // Start the coroutine to handle movement
        StartCoroutine(MoveUpAndReturnRoutine());
    }

    private void Awake()
    {
        delay = Random.Range(2f,4f);
        delay2 = Random.Range(1f, 3f);
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
            yield return new WaitForSeconds(delay2);

            // Move the object back to the original position
            transform.position = originalPosition;
        }
    }
}

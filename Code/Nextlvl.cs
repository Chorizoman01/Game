using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelDoor : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player has collided with the door
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if GameManager instance exists
            if (GameManager.Instance != null)
            {
                // Increase the world variable by 1
                GameManager.Instance.Nextlvl();

                // Load the next scene (e.g., Scene 2)
                LoadNextScene();
            }
            else
            {
                Debug.LogError("GameManager instance is missing!");
            }
        }
    }

    private void LoadNextScene()
    {
        // Get the current scene's index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the next scene by index
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}

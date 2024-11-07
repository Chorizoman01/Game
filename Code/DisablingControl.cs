using System.Collections;
using UnityEngine;
using TMPro;

public class DisablingControl : MonoBehaviour
{
    private NewJumpy jumpyMovement;  // Reference to the JumpyMovement script on the player
    private int currentDisruption = -1;   // Track the current disruption type (-1 means no disruption)

    public float disruptionInterval = 2f; // Interval in seconds to change disruptions
    public float controlRestorationTime = 5f; // Time in seconds for control restoration before next disruption
    public TextMeshProUGUI disruptionText; // Reference to the TMP text object in the UI

    private void Awake()
    {
        disruptionInterval = Random.Range(1f, 3f);
        controlRestorationTime = Random.Range(3f, 8f);

        // Get the JumpyMovement component attached to the same GameObject
        jumpyMovement = GetComponent<NewJumpy>();
        if (jumpyMovement == null)
        {
            Debug.LogError("JumpyMovement script not found on the player object!");
            enabled = false; // Disable this script if JumpyMovement is not found
            return;
        }

        if (disruptionText == null)
        {
            Debug.LogError("DisruptionText (TextMeshProUGUI) is not assigned!");
            return;
        }

        // Start the initial disruption coroutine
        StartCoroutine(ApplyRandomDisruptions());
    }

    private IEnumerator ApplyRandomDisruptions()
    {
        while (true)
        {
            // Choose a random disruption: 0 = disable jump, 1 = disable left, 2 = disable right, -1 = no disruption
            currentDisruption = Random.Range(0, 3);

            // Update the TMP text to show the current disruption
            UpdateDisruptionText();

            // Wait for the specified disruption interval
            yield return new WaitForSeconds(disruptionInterval);

            // Restore control for the specified restoration time
            currentDisruption = -1;  // Allow all actions
            UpdateDisruptionText();  // Update the TMP text to show all actions are enabled

            yield return new WaitForSeconds(controlRestorationTime);
        }
    }

    private void UpdateDisruptionText()
    {
        switch (currentDisruption)
        {
            case 0:
                disruptionText.text = "Jumping disabled";
                break;
            case 1:
                disruptionText.text = "Moving left disabled";
                break;
            case 2:
                disruptionText.text = "Moving right disabled";
                break;
            default:
                disruptionText.text = "All actions enabled";
                break;
        }
    }

    private void Update()
    {
        // Override inputs based on current disruption type
        float horizontalInput = Input.GetAxis("Horizontal");
        bool jumpInput = Input.GetButtonDown("Jump");

        // Handle disruption based on current disruption type
        switch (currentDisruption)
        {
            case 0: // Disable jump
                jumpInput = false; // Ignore jump input
                break;
            case 1: // Disable moving left
                if (horizontalInput < 0) horizontalInput = 0; // Block left movement
                break;
            case 2: // Disable moving right
                if (horizontalInput > 0) horizontalInput = 0; // Block right movement
                break;
            default:
                // No disruption; pass through inputs as normal
                break;
        }

        // Pass modified inputs to JumpyMovement
        jumpyMovement.SetInputs(horizontalInput, jumpInput);
    }
}

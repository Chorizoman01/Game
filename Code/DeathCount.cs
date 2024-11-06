using UnityEngine;
using TMPro;

public class DeathCount : MonoBehaviour
{
    private TextMeshProUGUI deathCounterText;

    private void Awake()
    {
        // Get the TextMeshProUGUI component attached to this object
        deathCounterText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (GameManager.Instance != null)
        {
            // Update the TMP text to display the current death count from GameManager
            deathCounterText.text = "x " + GameManager.Instance.deaths;
        }
        else
        {
            Debug.LogError("GameManager instance is missing!");
        }
    }
}

using UnityEngine;
using TMPro;

public class TimeSpent : MonoBehaviour
{
    public static TimeSpent Instance { get; private set; }  // Singleton instance
    public TextMeshProUGUI timerText; // Reference to the TMP text to display time

    private float elapsedTime;

    private void Awake()
    {
        // Singleton pattern: destroy duplicate instances
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set the singleton instance and make it persistent
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Ensure timerText is assigned, otherwise try finding it in children
        if (timerText == null)
        {
            timerText = GetComponentInChildren<TextMeshProUGUI>();
            if (timerText == null)
            {
                Debug.LogError("Timer Text (TMP) is not assigned in TimeSpent script!");
            }
        }
    }

    private void Update()
    {
        // Increment elapsed time by time since the last frame
        elapsedTime += Time.deltaTime;

        // Format elapsed time as minutes and seconds, then update the TMP text
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime % 60F);
        timerText.text = string.Format("x {0:00}:{1:00}", minutes, seconds);
    }
}

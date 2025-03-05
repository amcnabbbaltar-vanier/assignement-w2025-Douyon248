using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // For TextMeshPro support

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to the timer UI element
    private float timeElapsed = 0f; // Time in seconds

    private void Update()
    {
        // Count time in seconds
        timeElapsed += Time.deltaTime;

        // Update the timer UI text every frame
        float minutes = Mathf.Floor(timeElapsed / 60);
        float seconds = Mathf.FloorToInt(timeElapsed % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Method to get the current time
    public float GetCurrentTime()
    {
        return timeElapsed;
    }

    // Call this method at the end of the game to display the final time
    public void EndTimer()
    {
        // You can add this method to your GameManager or EndScreen
        string finalTime = string.Format("{0:00}:{1:00}", Mathf.Floor(timeElapsed / 60), Mathf.FloorToInt(timeElapsed % 60));
        Debug.Log("Final Time: " + finalTime);
    }
}

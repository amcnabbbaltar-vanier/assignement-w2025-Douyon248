using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // For TextMeshPro support

public class EndScreen : MonoBehaviour
{
    public GameObject endScreenUI; // Assign EndScreenCanvas here
    public TextMeshProUGUI finalScoreText; // Assign FinalScoreText here
    public TextMeshProUGUI finalTimeText; // Assign FinalTimeText here (for time display)

    // Function to display the end screen with the score and time
    public void ShowEndScreen(int score, float time)
    {
        finalScoreText.text = "Final Score: " + score.ToString();
        finalTimeText.text = "Time: " + string.Format("{0:00}:{1:00}", Mathf.Floor(time / 60), Mathf.FloorToInt(time % 60));
        endScreenUI.SetActive(true); // Activate the end screen UI
        Time.timeScale = 0f; // Pause the game
    }

    // Function to restart the game
    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume time
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene (restart level)
    }
}


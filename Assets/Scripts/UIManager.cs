using System.Collections;
using UnityEngine;
using TMPro; // Import TextMeshPro

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    // UI elements
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI TimerText; // Timer Text

    [Header("End Screen UI")]
    public GameObject endScreen; // End Screen Panel
    public TextMeshProUGUI finalScoreText;  // Displays final score
    public TextMeshProUGUI finalTimeText;   // Displays final time

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate UIManagers
        }
    }

    public void UpdateScore(int score)
    {
        if (ScoreText != null)
        {
            ScoreText.text = "Score: " + score;
        }
    }

    public void UpdateHealth(int health)
    {
        if (HealthText != null)
        {
            HealthText.text = "Health: " + health;
        }
    }

    public void UpdateTimer(float time)
    {
        if (TimerText != null)
        {
            TimerText.text = "Time: " + time.ToString("F2") + "s"; // Format time to 2 decimals
        }
    }

    public void ShowEndScreen(int finalScore, float finalTime)
    {
        if (endScreen != null)
        {
            endScreen.SetActive(true); // Show End Screen UI
        }

        if (finalScoreText != null)
        {
            finalScoreText.text = "Final Score: " + finalScore;
        }

        if (finalTimeText != null)
        {
            finalTimeText.text = "Final Time: " + finalTime.ToString("F2") + "s"; // Format time
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance of the GameManager
    public EndScreen endScreen; // Reference to the EndScreen script (drag your EndScreenManager here in Inspector)
    public Timer timer; // Reference to the Timer script (drag your Timer component here)
    public TextMeshProUGUI finalScoreText; // Reference to the score text in the UI

    private int playerScore = 0;
    private bool isGamePaused = false;

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep GameManager between scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Start()
    {
        // Initialize score text at the beginning
        UpdateScoreText();
    }

    // Call this function to increase the score when needed
    public void AddScore(int amount)
    {
        playerScore += amount;
        finalScoreText.text = "Score: " + playerScore.ToString();
    }

    // Update the score UI text
    private void UpdateScoreText()
    {
        finalScoreText.text = "Score: " + playerScore.ToString();
    }

    // Call this function to handle the Game Over logic
    public void GameOver()
    {
        // Get the current time from the Timer
        float finalTime = timer.GetCurrentTime();

        // Show end screen with final score and time
        endScreen.ShowEndScreen(playerScore, finalTime);

        // Pause the game when it's over
        PauseGame();
    }

    // Pause or unpause the game
    private void TogglePause()
    {
        if (isGamePaused)
        {
            UnpauseGame();
        }
        else
        {
            PauseGame();
        }
    }

    // Pause the game (stop time)
    private void PauseGame()
    {
        Time.timeScale = 0f; // Stop the game (freeze everything)
        isGamePaused = true;
    }

    // Unpause the game (resume time)
    private void UnpauseGame()
    {
        Time.timeScale = 1f; // Resume the game
        isGamePaused = false;
    }

    // Restart the current level
    public void RestartLevel()
    {
        Time.timeScale = 1f; // Unpause the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }

    // Load the main menu (or a specific scene)
    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Unpause the game before transitioning
        SceneManager.LoadScene(0); // Assuming the main menu is Scene 0
    }
}

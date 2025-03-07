using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Game-related variables
    public int score = 0;
    public Vector3 lastCheckpointPosition; // Store the checkpoint position

    private float gameStartTime;
    private bool isGameRunning;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure the GameManager persists across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate GameManagers
        }
    }

    private void Start()
    {
        gameStartTime = Time.time;
        isGameRunning = true;
    }

    private void Update()
    {
        if (isGameRunning)
        {
            // Check if UIManager.Instance is not null before using it
            if (UIManager.Instance != null)
            {
                UIManager.Instance.UpdateTimer(Time.time - gameStartTime);
            }
            else
            {
                Debug.LogError("UIManager.Instance is null. Make sure the UIManager is properly set up.");
            }
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateScore(score); // Ensure UI updates properly
        }
        Debug.Log("Score added: " + score); // Debug log to track score
    }

    public void FinishGame()
    {
        // Handle the game finish logic, e.g., show end screen
        Debug.Log("Game Finished!");
        // Optionally, you can call a UI method to show an end screen here.
        UIManager.Instance.ShowEndScreen(score, Time.time - gameStartTime); // Example end screen
        Time.timeScale = 0f; // Pause the game
    }

    public void ResetGame()
    {
        score = 0;  // Reset score
        lastCheckpointPosition = Vector3.zero;  // Reset checkpoint position
        ResetPickups(); // Reset all pickups in the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the scene
        isGameRunning = true;
        Time.timeScale = 1f; // Resume game time
        gameStartTime = Time.time;  // Reset the timer
    }

    public void ResetPickups()
    {
        // Find all pickups in the scene and destroy them
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");
        foreach (GameObject pickup in pickups)
        {
            Destroy(pickup); // Destroy the pickup object
        }
    }

    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        lastCheckpointPosition = checkpointPosition;
        Debug.Log("Checkpoint set at: " + checkpointPosition);
    }

    // New method to load the next level
    public void LoadNextLevel()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Check if there's a next scene in the build settings
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            // Load the next scene by index
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            // If it's the last level, finish the game (optional)
            FinishGame();
        }
    }
}

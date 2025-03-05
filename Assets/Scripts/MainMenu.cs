using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1); // Loads the first level (make sure it's added to Build Settings)
    }

    public void QuitGame()
    {
        Application.Quit(); 
        Debug.Log("Game Quit!"); 
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndGoal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has reached the end goal
        if (other.CompareTag("Player"))
        {
            // Load the next level using GameManager
            GameManager.Instance.LoadNextLevel();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance; // Singleton instance of PlayerHealth
    public int maxHealth = 3;
    private int currentHealth;

    private void Awake()
    {
        // Ensure that there's only one instance of PlayerHealth
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances if any
        }
    }

    private void Start()
    {
        currentHealth = maxHealth; // Initialize current health
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            // Trigger Game Over or Restart logic
            GameManager.Instance.GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedTrap"))
        {
            TakeDamage(1); // Lose 1 health
        }
    }

    // Method to access current health
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}

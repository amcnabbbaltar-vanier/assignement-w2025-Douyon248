using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Make sure this is included if you're using TextMeshPro

public class HealthUI : MonoBehaviour
{
    public TextMeshProUGUI healthText; // Reference to the health UI text
    private PlayerHealth playerHealth; // Reference to the PlayerHealth script

    private void Start()
    {
        // Find the player object and get the PlayerHealth component
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        // Update health display every frame
        if (playerHealth != null)
        {
            healthText.text = "Health: " + playerHealth.GetCurrentHealth();
        }
    }
}


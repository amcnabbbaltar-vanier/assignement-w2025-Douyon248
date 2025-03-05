using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private int scoreValue = 50; // Points to be added when collected

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Call GameManager to add the score
            GameManager.Instance.AddScore(scoreValue);

            // Trigger collection effect (e.g., disappear, hover effect, etc.)
            CollectPickup();

            // Destroy the pickup after collection
            Destroy(gameObject);
        }
    }

    private void CollectPickup()
    {
        // Add any additional effects here (e.g., particle effect)
    }
}


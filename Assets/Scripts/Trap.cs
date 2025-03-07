using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    // Damage to apply when the player collides with the trap
    public int damage = 1;

    // Method that runs when something enters the trap's trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Get the CharacterMovement component from the player
            CharacterMovement playerMovement = other.GetComponent<CharacterMovement>();

            // If the player has a CharacterMovement component, apply damage
            if (playerMovement != null)
            {
                playerMovement.TakeDamage(damage);  // Apply the damage to the player
            }
        }
    }
}


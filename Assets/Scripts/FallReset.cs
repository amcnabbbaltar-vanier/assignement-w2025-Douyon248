using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallReset : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has fallen off the level (e.g., below a certain height)
        if (transform.position.y < -10)
        {
            // Call ResetPosition to reset the character's position
            CharacterMovement characterMovement = other.GetComponent<CharacterMovement>();
            if (characterMovement != null)
            {
                characterMovement.ResetPosition(); // Reset position to last checkpoint
            }
        }
    }
}



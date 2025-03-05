using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickup : MonoBehaviour
{
    [SerializeField] private int scoreAmount = 50;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Use the corrected 'Instance' with an uppercase 'I'
            GameManager.Instance.AddScore(scoreAmount);
            Destroy(gameObject); // Destroy the score pickup after collection
        }
    }
}


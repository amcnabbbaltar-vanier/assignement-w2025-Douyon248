using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] private float boostMultiplier = 1.5f;
    [SerializeField] private float duration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterMovement player = other.GetComponent<CharacterMovement>();
            if (player)
            {
                StartCoroutine(ApplySpeedBoost(player));
            }
            Destroy(gameObject);
        }
    }

    private IEnumerator ApplySpeedBoost(CharacterMovement player)
    {
        player.speedMultiplier = boostMultiplier;
        yield return new WaitForSeconds(duration);
        player.speedMultiplier = 1f;
    }
}


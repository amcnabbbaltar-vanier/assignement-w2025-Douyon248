using System.Collections;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PickupType
    {
        SpeedBoost,
        JumpBoost,
        ScorePickup // New pickup type for score
    }

    public PickupType pickupType;
    public float boostDuration = 5f;
    public int scoreValue = 50; // Score amount for score pickups
    public ParticleSystem pickupEffect; // Assign a particle effect in the Inspector

    [Header("Hover & Rotate")]
    public float rotationSpeed = 50f;
    public float hoverSpeed = 1f;
    public float hoverHeight = 0.2f;

    private Vector3 startPos;
    private MeshRenderer meshRenderer;
    private Collider pickupCollider;

    private void Start()
    {
        gameObject.tag = "Pickup"; // Ensure the correct tag

        startPos = transform.position;
        meshRenderer = GetComponent<MeshRenderer>();
        pickupCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        RotatePickup();
        HoverPickup();
    }

    private void RotatePickup()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void HoverPickup()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterMovement playerMovement = other.GetComponent<CharacterMovement>();

        if (playerMovement != null)
        {
            switch (pickupType)
            {
                case PickupType.ScorePickup:
                    Debug.Log("Score pickup collected!"); // Debug log to ensure the score is being added
                    GameManager.Instance.AddScore(scoreValue); // Add score directly
                    break;
            }

            StartCoroutine(CollectPickup()); // Start collecting the pickup
        }
    }

    private IEnumerator CollectPickup()
    {
        pickupCollider.enabled = false; // Disable collider to prevent multiple pickups
        if (meshRenderer != null) meshRenderer.enabled = false; // Hide the pickup mesh

        if (pickupEffect != null)
        {
            // Instantiate and play the pickup effect
            ParticleSystem effect = Instantiate(pickupEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration); // Destroy the effect after it finishes
        }

        yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds

        Destroy(gameObject); // Destroy the pickup object
    }
}

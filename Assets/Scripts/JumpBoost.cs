using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : MonoBehaviour
{
    [SerializeField] private float duration = 30f;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterMovement player = other.GetComponent<CharacterMovement>();
            if (player)
            {
                StartCoroutine(EnableDoubleJump(player));
            }
            Destroy(gameObject);
        }
    }

    public IEnumerator EnableDoubleJump(CharacterMovement player)
    {
        player.canDoubleJump = true;
        yield return new WaitForSeconds(duration);
        player.canDoubleJump = false;
    }
}

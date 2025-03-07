using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator animator;
    private CharacterMovement characterMovement;

    public void Start()
    {
        animator = GetComponent<Animator>();
        characterMovement = GetComponent<CharacterMovement>();
    }

    public void LateUpdate()
    {
        UpdateAnimator();
    }

    // TODO Fill this in with your animator calls
    void UpdateAnimator()
    {
        animator.SetFloat("Speed", characterMovement.groundSpeed);

        // Check if the character is grounded
        if (characterMovement.IsGrounded)
        {
            animator.SetBool("IsJumping", false); // Player is not jumping if grounded
        }

        // Check for double jump status using the public property
        if (characterMovement.HasDoubleJumped) // Accessing the public property
        {
            animator.SetTrigger("DoubleJump");
        }
        else
        {
            animator.SetBool("IsJumping", true); // Set jumping animation if the player is jumping
        }
    }
}

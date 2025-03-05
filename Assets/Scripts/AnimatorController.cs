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

        if (characterMovement.IsGrounded)
        {
            animator.SetBool("IsJumping", false);
        }

        if (characterMovement.hasDoubleJumped)
        {
            animator.SetTrigger("DoubleJump");
        }
        else
        {
            animator.SetBool("IsJumping", true);
        }
    }


}

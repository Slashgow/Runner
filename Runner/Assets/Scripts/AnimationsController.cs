using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInputs))]
public class AnimationsController : MonoBehaviour
{
    private Animator animator;
    private PlayerInputs playerInputs;

    private readonly int IS_JUMPING = Animator.StringToHash("isJumping");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerInputs = GetComponent<PlayerInputs>();
    }

    private void Update()
    {
        if (!animator.GetBool(IS_JUMPING) && !playerInputs.IsGrounded)
            animator.SetBool(IS_JUMPING, true);

        else if(animator.GetBool(IS_JUMPING) && playerInputs.IsGrounded)
            animator.SetBool(IS_JUMPING, false);

    }

}

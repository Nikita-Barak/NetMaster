using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriter;
    private string currentAnimation;
    private bool isDead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        SetAnimation(animations[0], false);
    }

    // The animations are 8 in number, constant and won't change. Therefore we choose specific indices that correspond to the matching animation.
    public void HandleMovement(float horizontal, float vertical, bool resetAnimation)
    {
        if (!isDead)
        {
            string newAnimation;

            if (vertical > 0)
            {
                if (horizontal > 0)
                {
                    spriter.flipX = false;
                    newAnimation = "Up_Diag";
                }
                else if (horizontal < 0)
                {
                    spriter.flipX = true;
                    newAnimation = "Up_Diag";
                }
                else
                {
                    newAnimation = "Up";
                }
            }
            else if (vertical < 0)
            {
                if (horizontal > 0)
                {
                    spriter.flipX = false;
                    newAnimation = "Down_Diag";
                }
                else if (horizontal < 0)
                {
                    spriter.flipX = true;
                    newAnimation = "Down_Diag";
                }
                else
                {
                    newAnimation = "Down";
                }
            }
            else
            {
                if (horizontal != 0)
                {
                    newAnimation = "Horizontal";
                }
                else
                {
                    return;
                }
            }

            SetAnimation(newAnimation, resetAnimation);
        }
    }

    public void HandleDeath()
    {
        isDead = true;
        SetAnimation("Death", false);
    }

    public void SetAnimation(string anim, bool reset)
    {
        if (anim == currentAnimation && !reset)
        {
            return;
        }
        if (reset)
        {
            animator.Play(anim, 0, 0); // Reset the animation
        }
        else
        {
            animator.Play(anim);
        }

        currentAnimation = anim;
    }
}
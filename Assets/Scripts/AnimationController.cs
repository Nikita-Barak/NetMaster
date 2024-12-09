using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriter;
    private string currentAnimation;
    private bool isDead = false;

    private readonly List<string> animations = new List<string>
    {
        "Down",
        "Down_Diag",
        "Up_Diag",
        "Up",
        "Horizontal"
    };

    private readonly string death = "Death";

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        SetAnimation(animations[0], false);
    }

    public void HandleMovement(float horizontal, float vertical, bool resetAnimation)
    {
        if(!isDead)
        {
            string newAnimation;

            if (vertical > 0)
            {
                if(horizontal > 0)
                {
                    spriter.flipX = true;
                    newAnimation = animations[2];
                }
                else if(horizontal < 0)
                {
                    spriter.flipX = false;
                    newAnimation = animations[2];
                }
                else
                {
                    newAnimation = animations[3];
                }
            }
            else if (vertical < 0)
            {
                if(horizontal > 0)
                {
                    spriter.flipX = false;
                    newAnimation = animations[1];
                }
                else if(horizontal < 0)
                {
                    spriter.flipX = true;
                    newAnimation = animations[1];
                }
                else
                {
                    newAnimation = animations[0];
                } 
            }
            else
            {
                if(horizontal != 0)
                {
                    newAnimation = animations[4];
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
        SetAnimation(death, false);
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
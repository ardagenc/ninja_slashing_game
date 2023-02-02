using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public Movement movement;
    public Animator animator;
    public Rigidbody2D rb;

    private string currentAnimaton;

    public float mouseAngle;

    const string Idle = "IdleAnimation";
    const string IdleFlipped = "IdleAnimFlipped";
    const string IdleDown = "IdleAnimDown";
    const string IdleUp = "IdleAnimUp";
    const string Run = "RunAnimation";
    const string RunFlipped = "RunAnimFlipped";
    const string RunDown = "RunAnimDown";
    const string RunUp = "RunAnimUp";
    const string Dash = "DashAnim";
    const string DashFlipped = "DashAnimFlipped";
    const string DashDown = "DashAnimDown";
    const string DashUp = "DashAnimUp";

    void Start()
    {
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        mouseAngle = Mathf.Atan2(movement.distanceBetweenObj.y, movement.distanceBetweenObj.x) * Mathf.Rad2Deg;


        if (movement.state == Movement.State.Normal)
        {//Idle Anim
            if (rb.velocity.magnitude <= 0 && mouseAngle >= 0)
            {
                if (mouseAngle < 45)
                {
                    ChangeAnimationState(Idle);
                }
                else if (mouseAngle < 135)
                {
                    ChangeAnimationState(IdleUp);
                }
                else if (mouseAngle < 180)
                {
                    ChangeAnimationState(IdleFlipped);
                }

            }
            else if (rb.velocity.magnitude <= 0 && mouseAngle < 0)
            {
                if (mouseAngle > -45)
                {
                    ChangeAnimationState(Idle);
                }
                else if (mouseAngle > -135)
                {
                    ChangeAnimationState(IdleDown);
                }
                else if (mouseAngle > -180)
                {
                    ChangeAnimationState(IdleFlipped);
                }
            }
        }

        if (movement.state == Movement.State.Normal)
        {
            if (rb.velocity.magnitude > 0 && mouseAngle >= 0)
            {
                if (mouseAngle < 45)
                {
                    ChangeAnimationState(Run);
                }
                else if (mouseAngle < 135)
                {
                    ChangeAnimationState(RunUp);
                }
                else if (mouseAngle < 180)
                {
                    ChangeAnimationState(RunFlipped);
                }

            }
            else if (rb.velocity.magnitude > 0 && mouseAngle < 0)
            {
                if (mouseAngle > -45)
                {
                    ChangeAnimationState(Run);
                }
                else if (mouseAngle > -135)
                {
                    ChangeAnimationState(RunDown);
                }
                else if (mouseAngle > -180)
                {
                    ChangeAnimationState(RunFlipped);
                }
            }
        }

        if(movement.state == Movement.State.Rolling)
        {
            if (mouseAngle >= 0)
            {
                if (mouseAngle < 45)
                {
                    ChangeAnimationState(Dash);
                }
                else if (mouseAngle < 135)
                {
                    ChangeAnimationState(DashUp);
                }
                else if (mouseAngle < 180)
                {
                    ChangeAnimationState(DashFlipped);
                }

            }
            else if (mouseAngle < 0)
            {
                if (mouseAngle > -45)
                {
                    ChangeAnimationState(Dash);
                }
                else if (mouseAngle > -135)
                {
                    ChangeAnimationState(DashDown);
                }
                else if (mouseAngle > -180)
                {
                    ChangeAnimationState(DashFlipped);
                }
            }
        }
    }

    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimaton = newAnimation;
    }
}

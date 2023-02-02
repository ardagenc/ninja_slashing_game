using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAnimation : MonoBehaviour
{
    public EnemyArcher ArcherMovement;
    public Animator ArcherAnimator;
    public Rigidbody2D ArcherRb;

    private string currentAnimaton;

    const string ArcherIdle = "ArcherIdle";
    const string ArcherRun = "ArcherRun";
    const string ArcherShotPrep = "ArcherShotPrep";
    const string ArcherShot = "ArcherShot";

    void Start()
    {
        ArcherMovement = GetComponent<EnemyArcher>();
        ArcherAnimator = GetComponent<Animator>();
        ArcherRb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if(ArcherMovement.enemyArcherState == EnemyArcher.State.enemyArcherNormal)
        {
            if(ArcherRb.velocity.magnitude <= 0)
            {
                ChangeAnimationState(ArcherIdle);
            }
            else if(ArcherRb.velocity.magnitude > 0)
            {
                ChangeAnimationState(ArcherRun);
            }
        }

        if(ArcherMovement.enemyArcherState == EnemyArcher.State.enemyArcherPrep)
        {
            ChangeAnimationState(ArcherShotPrep);
        }

        if (ArcherMovement.enemyArcherState == EnemyArcher.State.enemyArcherAttack)
        {
            ChangeAnimationState(ArcherShot);
        }



    }
    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;

        ArcherAnimator.Play(newAnimation);
        currentAnimaton = newAnimation;
    }

}

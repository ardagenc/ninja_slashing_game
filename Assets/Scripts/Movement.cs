using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class Movement : MonoBehaviour
{
    public ParticleSystem blood;
    public enum State
    {
        Normal,
        Rolling,
        Cooldown,
        Dead,
    }
    
    public State state;

    private Rigidbody2D rb;
    private Movement playerMovement;    
    private Animator anim;

    private Vector2 mouse;
    private Vector2 moveDir;

    private Vector2 rollDir;
    public float rollSpeed;
    public float rollSpeedMax;
    public float rollSpeedDropMultiplier;
    public float rollSpeedMinimum;

    public float rollCooldown;
    public float rollCooldownMax;

    private bool facingRight = true;

    public Vector2 distanceBetweenObj;


    public float mouseDistance;
    public float moveSpeed;

    public int playerHealth;
    public int playerAttack;

    

    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<Movement>();        
        anim = GetComponent<Animator>();

        state = State.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Normal:

                mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                distanceBetweenObj = new Vector2(mouse.x - transform.position.x, mouse.y - transform.position.y);

                moveDir = new Vector2(mouse.x - transform.position.x, mouse.y - transform.position.y);
                moveDir = moveDir.normalized;

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    rollDir = moveDir;
                    state = State.Rolling;
                }

                break;

            case State.Rolling:


                rollSpeed -= rollSpeed * rollSpeedDropMultiplier * Time.deltaTime;


                if (rollSpeed < rollSpeedMinimum)
                {                              
                    state = State.Cooldown;
                    rollSpeed = rollSpeedMax;
                    
                }
                break;

            case State.Cooldown:

                rollCooldown -= Time.deltaTime;

                if(rollCooldown <= 0)
                {
                    state = State.Normal;
                    rollCooldown = rollCooldownMax;
                }

                break;

        }
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Normal:

                if (distanceBetweenObj.magnitude > mouseDistance)
                {
                    rb.velocity = moveDir * moveSpeed;
                    //anim.SetBool("isRunning", true);
                }
                else
                {
                    rb.velocity = moveDir * 0;
                   // anim.SetBool("isRunning", false);
                }

                break;
            case State.Rolling:


                rb.velocity = rollDir * rollSpeed;

                break;

            case State.Cooldown:

                rb.velocity = moveDir * 0;

                rollCooldown -= Time.deltaTime;

                //anim.SetBool("isRunning", false);


                break;

        }
    }

    public void BloodFX()
    {
        blood.Play();
    }
    public void TakeDamage(int attack)
    {
        BloodFX();

        playerHealth -= attack;

        if(playerHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        playerMovement.enabled = false;
        rb.velocity = moveDir * 0;
        anim.SetBool("isDead", true);
    }
  
    public void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public enum State
    {
        enemyNormal,
        enemyPrep,
        enemyAttack,
        enemyWait,
        enemyDead,
    }

    public ParticleSystem blood;

    public State enemyState;    

    private EnemyScript enemyScript;
    private Collider2D enemyCollider;
    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 enemyRunDirection;
    private Vector2 enemyDashDirection;

    private Vector2 distanceBetweenPlayer;

    private bool facingRight = true;

    public float moveSpeed;

    public float dashSpeed;
    public float dashSpeedMin;
    public float dashSpeedMax;
    public float dashSpeedDropMultiplier;

    public float dashTriggerDistance;

    public float enemyPrepTime;
    public float enemyPrepTimeMax;

    public float enemyCooldown;
    public float enemyCooldownMax;

    public int health;
    public int enemyAttack;

    public int lifetime;

    [SerializeField] Transform playerLocation;
    Wavespawner enemyDied;
    [SerializeField] GameObject waveSpawner;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
        enemyScript = GetComponent<EnemyScript>();
        anim = GetComponent<Animator>();
        playerLocation = GameObject.Find("Player").transform;
        waveSpawner = GameObject.Find("WaveSpawner");
        enemyDied = waveSpawner.GetComponent<Wavespawner>();

        enemyState = State.enemyNormal;
    }
    void Update()
    {
        switch (enemyState)
        {
            case State.enemyNormal:
            
                playerLocation = playerLocation.transform;

                

                enemyRunDirection = new Vector2(playerLocation.position.x - transform.position.x,
                    playerLocation.position.y - transform.position.y);

                distanceBetweenPlayer = enemyRunDirection;

                enemyRunDirection = enemyRunDirection.normalized;

                if (distanceBetweenPlayer.magnitude <= dashTriggerDistance)
                {
                    enemyState = State.enemyPrep;
                }


                break;

            case State.enemyPrep:

                enemyPrepTime -= Time.deltaTime;

                distanceBetweenPlayer = enemyRunDirection;

                enemyRunDirection = new Vector2(playerLocation.position.x - transform.position.x,
                    playerLocation.position.y - transform.position.y);

                enemyDashDirection = enemyRunDirection.normalized;

                if (enemyPrepTime <= 0)
                {
                    enemyState = State.enemyAttack;
                    enemyPrepTime = enemyPrepTimeMax;
                }

            break;

            case State.enemyAttack:

                dashSpeed -= dashSpeed * dashSpeedDropMultiplier * Time.deltaTime;

                if (dashSpeed < dashSpeedMin)
                {
                    enemyState = State.enemyWait;
                    dashSpeed = dashSpeedMax;
                }


            break;

            case State.enemyWait:

                //kötü kod gibi
                distanceBetweenPlayer = new Vector2(playerLocation.position.x - transform.position.x,
                    playerLocation.position.y - transform.position.y);

                enemyCooldown -= Time.deltaTime;

                if (enemyCooldown <= 0)
                {
                    enemyState = State.enemyNormal;
                    enemyCooldown = enemyCooldownMax;
                }
            break;

            case State.enemyDead:

                anim.SetBool("isDead", true);

                enemyScript.enabled = false;
                Invoke("DestroyObject", lifetime);                

                break;
        }

        if (Mathf.Sign(distanceBetweenPlayer.x) <= 0 && facingRight)
        {
            Flip();
        }
        else if (Mathf.Sign(distanceBetweenPlayer.x) >= 0 && !facingRight)
        {
            Flip();
        }

    }

    private void FixedUpdate()
    {
        switch (enemyState)
        {
            case State.enemyNormal:
                
                rb.velocity = enemyRunDirection * moveSpeed;

                if(rb.velocity == new Vector2(0, 0))
                {
                    anim.SetBool("isRunning", false);
                }
                else
                {
                    anim.SetBool("isRunning", true);
                }

            break;

            case State.enemyPrep:

                //geçici animasyon
                anim.SetBool("isRunning", false);

                rb.velocity = new Vector2(0, 0);

            break;

            case State.enemyAttack:

                rb.velocity = enemyDashDirection * dashSpeed;

            break;

            case State.enemyWait:

                //geçici animasyon
                anim.SetBool("isRunning", false);

                rb.velocity = new Vector2(0, 0);

                break;

            case State.enemyDead:

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

        health -= attack;

        if(health <= 0)
        {
            enemyDied.deadEnemyCount++;
            enemyDied.increaseDeadEnemyCountMax++;
            enemyDied.enemyCount--;

            Die();
        }
    }
    public void Die()
    {
        rb.velocity = new Vector2(0, 0);

        enemyState = State.enemyDead;

    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

}

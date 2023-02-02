using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcher : MonoBehaviour
{
    public ParticleSystem blood;
    public enum State
    {
        enemyArcherNormal,
        enemyArcherPrep,
        enemyArcherAttack,
        enemyArcherWait,
        enemyArcherDead,
    }
    public State enemyArcherState;

    private EnemyArcher enemyArcher;
    private Collider2D enemyCollider;
    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 enemyRunDirection;
    public Vector2 enemyAttackDirection;

    private Vector2 distanceBetweenPlayer;


    private bool facingRight = true;

    public float moveSpeed;

    public float attackTriggerDistance;

    public float enemyArcherPrepTime;
    public float enemyArcherPrepTimeMax;

    public float enemyArcherCooldown;
    public float enemyArcherCooldownMax;

    public int health;
    public int enemyAttack;

    public int lifetime;

    //

    [SerializeField] Transform playerLocation;
    Wavespawner enemyDied;
    [SerializeField] GameObject waveSpawner;
    [SerializeField] GameObject arrow;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
        enemyArcher = GetComponent<EnemyArcher>();
        anim = GetComponent<Animator>();
        playerLocation = GameObject.Find("Player").transform;
        waveSpawner = GameObject.Find("WaveSpawner");
        enemyDied = waveSpawner.GetComponent<Wavespawner>();

        enemyArcherState = State.enemyArcherNormal;
    }

    void Update()
    {
        switch (enemyArcherState)
        {
            case State.enemyArcherNormal:

                playerLocation = playerLocation.transform;

                enemyRunDirection = new Vector2(playerLocation.position.x - transform.position.x,
                    playerLocation.position.y - transform.position.y);

                distanceBetweenPlayer = enemyRunDirection;

                enemyRunDirection = enemyRunDirection.normalized;

                if (distanceBetweenPlayer.magnitude <= attackTriggerDistance)
                {
                    enemyArcherState = State.enemyArcherPrep;
                }

                break;

            case State.enemyArcherPrep:

                enemyArcherPrepTime -= Time.deltaTime;

                distanceBetweenPlayer = enemyRunDirection;

                enemyRunDirection = new Vector2(playerLocation.position.x - transform.position.x,
                    playerLocation.position.y - transform.position.y);

                enemyAttackDirection = enemyRunDirection.normalized;

                if(enemyArcherPrepTime <= 0)
                {
                    enemyArcherState = State.enemyArcherAttack;
                    enemyArcherPrepTime = enemyArcherPrepTimeMax;
                }

                break;

            case State.enemyArcherAttack:

                break;

            case State.enemyArcherWait:

                distanceBetweenPlayer = new Vector2(playerLocation.position.x - transform.position.x,
                    playerLocation.position.y - transform.position.y);

                enemyArcherCooldown -= Time.deltaTime;

                if(enemyArcherCooldown <= 0)
                {
                    enemyArcherState = State.enemyArcherNormal;
                    enemyArcherCooldown = enemyArcherCooldownMax;
                }

                break;

            case State.enemyArcherDead:                            

                Invoke("DestroyObject", lifetime);
                enemyArcher.enabled = false;

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
        switch (enemyArcherState)
        {
            case State.enemyArcherNormal:

                rb.velocity = enemyRunDirection * moveSpeed;                           

                break;

            case State.enemyArcherPrep:

                rb.velocity = new Vector2(0, 0);

                break;

            case State.enemyArcherAttack:

                rb.velocity = new Vector2(0, 0);

                Instantiate(arrow, rb.position, transform.rotation);

                enemyArcherState = State.enemyArcherWait;

                break;

            case State.enemyArcherWait:

                rb.velocity = new Vector2(0, 0);

                break;

            case State.enemyArcherDead:
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

        if (health <= 0)
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

        enemyArcherState = State.enemyArcherDead;

        anim.SetBool("isDead", true);

    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }


    public void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }


}

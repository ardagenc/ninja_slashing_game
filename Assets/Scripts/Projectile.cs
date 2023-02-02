using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    /*
    public enum ArrowState 
    {
        arrowPrep,
        arrowFired,
    }

    private ArrowState arrowState;
    */
    public Rigidbody2D rb;

    public float arrowSpeed;
    public int arrowDamage;

    public float lifeTime;

    private Transform targetTransform;
    private Vector2 target;
    [SerializeField] EnemyArcher enemyArcher;
    [SerializeField] Transform playerLocation;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerLocation = GameObject.Find("Player").transform;
        enemyArcher = GetComponent<EnemyArcher>();
        targetTransform = GetComponent<Transform>();


        Vector2 direction = (playerLocation.position - transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        targetTransform.eulerAngles = new Vector3(0, 0, angle);


        target = new Vector2(playerLocation.position.x - rb.position.x, playerLocation.position.y - rb.position.y);
        target = target.normalized;

        Invoke("DestroyProjectile", lifeTime);
        ArrowShot();
        
    }
    public void ArrowShot()
    {
        rb.velocity = target * arrowSpeed;
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }


    public void OnTriggerEnter2D(Collider2D hitBox)
    {
        if (hitBox.CompareTag("playerHurtBox"))
        {
            hitBox.GetComponentInParent<Movement>().TakeDamage(arrowDamage);
        }
    }

}

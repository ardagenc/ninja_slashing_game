using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    EnemyScript enemyScript;
    Transform playerLocation;


    private EnemyHitBox enemyHitBox;
    private Transform aimTransform;
    public Collider2D hitBox;
    // Start is called before the first frame update
    void Awake()
    {
        aimTransform = GetComponent<Transform>();
        enemyScript = enemy.GetComponent<EnemyScript>();
        
        playerLocation = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyScript.enemyState != EnemyScript.State.enemyAttack)
        {
            hitBox.enabled = false;
        }

        if (enemyScript.enemyState == EnemyScript.State.enemyPrep)
        {
            Vector2 direction = (playerLocation.position - transform.position);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            aimTransform.eulerAngles = new Vector3(0, 0, angle);
        }
        else if (enemyScript.enemyState == EnemyScript.State.enemyAttack)
        {
            hitBox.enabled = true;                  
        }
        

    }

    public void OnTriggerEnter2D(Collider2D hitBox)
    {
        if (hitBox.CompareTag("playerHurtBox"))
        {
            hitBox.GetComponentInParent<Movement>().TakeDamage(enemyScript.enemyAttack);
        }
    }


}

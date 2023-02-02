using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHurtBox : MonoBehaviour
{
    private EnemyScript enemyScript;
    [SerializeField] GameObject enemy;
    public Collider2D hurtBox;

    // Start is called before the first frame update
    void Start()
    {
        enemyScript = enemy.GetComponent<EnemyScript>();
        hurtBox = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyScript.enemyState == EnemyScript.State.enemyDead)
        {
            hurtBox.enabled = false;
        }
        else
        {
            hurtBox.enabled = true;
        }

        
    }


}

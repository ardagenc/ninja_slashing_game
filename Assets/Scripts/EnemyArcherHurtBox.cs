using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcherHurtBox : MonoBehaviour
{
    private EnemyArcher enemyArcherScript;
    [SerializeField] GameObject enemy;
    public Collider2D hurtBox;

    // Start is called before the first frame update
    void Start()
    {
        enemyArcherScript = enemy.GetComponent<EnemyArcher>();
        hurtBox = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyArcherScript.enemyArcherState == EnemyArcher.State.enemyArcherDead)
        {
            hurtBox.enabled = false;
        }
        else
        {
            hurtBox.enabled = true;
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitBoxScript : MonoBehaviour
{
    private Movement movement;
    [SerializeField] GameObject player;

    private Transform aimTransform;
    public Collider2D hitBox;

    // Start is called before the first frame update
    private void Awake()
    {
        aimTransform = GetComponent<Transform>();        
        movement = player.GetComponent<Movement>();
        
       
    }

    // Update is called once per frame
    void Update()
    {        
        if(movement.state != Movement.State.Rolling)
        {
            hitBox.enabled = false;
        }

        if(movement.state == Movement.State.Normal)
        {            
            Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            aimTransform.eulerAngles = new Vector3(0, 0, angle);            
        }
        else if(movement.state == Movement.State.Rolling)
        {
            hitBox.enabled = true;            
        }        
    }

    public void OnTriggerEnter2D(Collider2D hitBox)
    {
        if (hitBox.CompareTag("enemyHurtBox"))
        {
            hitBox.GetComponentInParent<EnemyScript>().TakeDamage(movement.playerAttack);            
        }

        if (hitBox.CompareTag("enemyArcherHurtBox"))
        {
            hitBox.GetComponentInParent<EnemyArcher>().TakeDamage(movement.playerAttack);
        }

        if (hitBox.CompareTag("arrowHitBox"))
        {
            hitBox.GetComponent<Projectile>().DestroyProjectile();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHurtBox : MonoBehaviour
{
    private Movement movement;
    [SerializeField] GameObject player;
    

    public Collider2D hurtBox;
    // Start is called before the first frame update
    void Start()
    {
        movement = player.GetComponent<Movement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movement.state == Movement.State.Rolling)
        {
            hurtBox.enabled = false;
        }
        else
        {
            hurtBox.enabled = true;
        }

        if (movement.playerHealth <= 0)
        {
            hurtBox.enabled = false;
        }
    }

}

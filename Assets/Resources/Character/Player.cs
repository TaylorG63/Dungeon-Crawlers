using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Variables")]
    public float Speed = 4f;
    public float AttackCooldown = 1f;
    public byte MaxHealth = 10;
    public byte MaxMana = 10;
    public float AttackRange = 0.45f;
    public byte AttackDamage = 1;

    [Header("Components")]
    public Rigidbody2D Rigid;
    public Animator Animate;
    public Transform AttackPoint;
    public LayerMask EnemyLayers;

    private Vector2 movement;
    private byte attackStage;
    private float cooldownTimer = 0f;
    private float slowMove = 1;
    private byte health;
    //private byte mana;
    
    private void Start()
    {
        if (Animate == null)
        {
            Animate = gameObject.GetComponent<Animator>();
        }
        if(Rigid == null)
        {
            Rigid = gameObject.GetComponent<Rigidbody2D>();
        }
        health = MaxHealth;
        //mana = MaxMana;
        cooldownTimer = Time.time;
    }
    //private void FixedUpdate()
    //{
    //    Movement();
    //}

    void Update()
    {
        // We check if the player is moving and assign the input the the movement private Vector2 to assign to our movement script
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        // Check if the player is attacking
        Attacking();
    }

    #region Attack functions
    public void attack1()
    {
        if (attackStage >=2)
        {
            Animate.SetBool("Attack2", true);
        }
        else
        {
            Animate.SetBool("Attack1", false);
            attackStage = 0;
        }
    }

    public void attack2()
    {
        if (attackStage >= 3)
        {
            Animate.SetBool("Attack3", true);
        }
        else
        {
            Animate.SetBool("Attack2", false);
            attackStage = 0;
        }
    }

    public void attack3()
    {
        Animate.SetBool("Attack1", false);
        Animate.SetBool("Attack2", false);
        Animate.SetBool("Attack3", false);
        attackStage = 0;
    }

    private void Attacking()
    {
        if (Time.time - cooldownTimer > AttackCooldown) //Checks to see if enough time has passed for us to attack
        {
            attackStage = 0; //reset attckstage to 0
        }

        if (Input.GetMouseButtonDown(0)) // mouse1 is our attack button so this only activates when out mousebutton is pressed down (using axis results in needing superhuman timing to get result in less than a full combo)
        {
            cooldownTimer = Time.time; // sets our cooldown timer so we cannot spam attacks
            attackStage++; // adds one to our attack stage everytime the mouse is clicked
            if (attackStage == 1)
            {
                Animate.SetBool("Attack1", true); //sets our Attack1 variable in Animate to start attack animation
            }
            attackStage = (byte)Mathf.Clamp(attackStage, 0, 3); //makes sure our attackStage variable is never below 0 and above 3
            slowMove = 0.5f; //slow the players movement when attacking to make player more vulnerable when attacking
        }
    } // responsible for starting our attack sequence and checking if our cooldown for attacking is ready

    public void FinishAttack()
    {
        slowMove = 1;
    } //Return our movement speed to normal

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(AttackDamage);
        }
    }
    #endregion

    public float GetSlowMove()
    {
        return slowMove;
    }

    public void TakeDamage(byte dmg)
    {
        health -= dmg;
        if (health <=0)
        {
            Animate.SetBool("Dead", true);
        }
        Animate.SetTrigger("Hit");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}

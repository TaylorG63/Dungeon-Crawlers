using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public int MaxHealth = 2;
    public float Vision = 3;


    public LayerMask AI_Target;

    private Animator animate;
    private int currentHealth;
    private Vector2 playerPos;
    private Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        animate = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        currentHealth = MaxHealth;
        playerPos = Vector2.zero;
    }

    private void FixedUpdate()
    {
        //look();
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            animate.SetBool("Dead", true);
        }
        animate.SetTrigger("Hit");
    }

    //private void look()
    //{
    //    Collider2D Player = Physics2D.OverlapCircle(transform.position, Vision, AI_Target);
    //    if (Player != null)
    //    {
    //        playerPos.x = Player.transform.position.x;
    //        playerPos.y = Player.transform.position.y;
    //    }
    //    else
    //    {
    //        playerPos = new Vector2();
    //    }
        
    //}

    

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, Vision);
    //}
}

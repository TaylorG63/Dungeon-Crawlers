using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Variables")]
    public float Speed = 4f;

    [Header("Components")]
    public Rigidbody2D Rigid;
    public Animator Animate;
    public Player player;
    public Camera playerCamera;


    private Vector2 movement;
    private float slowMove = 1;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        // We check if the player is moving and assign the input the the movement private Vector2 to assign to our movement script
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        slowMove = player.GetSlowMove();
        // Check if the player is attacking
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (movement != Vector2.zero) //We want to move only when our Vector2
        {
            if (movement.x > 0) //We check if we are moving right
            {
                gameObject.transform.localScale = new Vector2(Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y); //if we are moving right change the X scale to face the sprite to the right
            }
            else if (movement.x < 0)
            {
                gameObject.transform.localScale = new Vector2(Mathf.Abs(gameObject.transform.localScale.x)*-1, gameObject.transform.localScale.y); //otherwise we face the left
            }
            Rigid.MovePosition(Rigid.position + (Vector2.ClampMagnitude(movement, 1f) * Speed * Time.deltaTime * slowMove)); //Apply movement to the player
            Animate.SetBool("Moving", true); //play the movement animation
            playerCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);
        }
        else
        {
            Animate.SetBool("Moving", false); //stop the moving animation
        }
    } //Controls the player movement
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontalInput;

    public float moveSpeed = 10f;
    public float jumpSpeed = 8f;
    public Transform groundCheckPoint; // we really just need to get the position of an object, below the player, to determine where to check for the ground.
    public LayerMask groundLayer;
    private float groundCheckRadius = 0.2f;
    public int maxJumps = 2;
    public int numJumps = 0;

    bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
    }

 
    void Start()
    {
        //because we are using physics, we need the rigidbody
        rb = GetComponent<Rigidbody2D>();
      
    }
    void Update()
    {
        //we need the ability to move left and right
        horizontalInput = Input.GetAxisRaw("Horizontal");
        
       // if (horizontalInput > 0)
       // {
        //    transform.localScale = new Vector3(2, 2, 1); //flip it to the right
       // }
       // else if (horizontalInput < 0)
       // {
       //     transform.localScale = new Vector3(-2, 2, 1); //flip it to the left
       // }
        
        float nextVelocityX = horizontalInput * moveSpeed;
        //we also need the ability to jump up
        float nextVelocityY = rb.velocity.y;
        if (GroundCheck() && nextVelocityY <= 0)
        {
            numJumps = maxJumps;
        }
        if (Input.GetKeyDown(KeyCode.Space) && numJumps > 0)
        {
            nextVelocityY = jumpSpeed;
            numJumps -= 1;
        }
        //we need to set the velocity of the rigidbody
        rb.velocity = new Vector2(nextVelocityX, nextVelocityY);

    }
}

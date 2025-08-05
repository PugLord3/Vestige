using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontalInput;
    public EffectManager em;
    public GameObject CP;
    public LayerMask death;
    public bool tutorial;
    public float TPCD = 2f;
    public bool candash = true;
    private bool cantp = true;
    public float moveSpeed = 10f;
    public float slamsped = 10f;
    public bool slamming = false;
    public bool canslam = true;
    public float dashspeed = 4f;
    public float jumpSpeed = 8f;
    public Transform groundCheckPoint; // we really just need to get the position of an object, below the player, to determine where to check for the ground.
    public LayerMask groundLayer;
    private float groundCheckRadius = 0.2f;
    private bool dashing = false;
    public int maxJumps = 2;
    public int numJumps = 0;

    bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
    }
    bool deathcheck()
    {
        return Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, death);
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
        if (deathcheck())
        {
            if(tutorial)
            {
                transform.position = CP.transform.position;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && numJumps > 0)
        {
            nextVelocityY = jumpSpeed;
            numJumps -= 1;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && cantp)
        {
            
              Vector3 mouseScreenPosition = Input.mousePosition;
              Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.nearClipPlane));
              Vector3 spot = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, transform.position.z);
           
              StartCoroutine(tp(spot));

           
        }
        if (Input.GetKeyDown(KeyCode.Q) && candash)
        {
            print("1");
            StartCoroutine(ActivateDash());
        }
        if (Input.GetKeyDown(KeyCode.E) && canslam)
        {
            
            StartCoroutine(ActivateSlam());
        }
        //we need to set the velocity of the rigidbody
        if(dashing)
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.nearClipPlane));
            Vector2 target = gameObject.transform.position - mouseWorldPosition;
            float x = target.x * dashspeed;
         
            float y = target.y * dashspeed;
            rb.velocity = new Vector2(-x, -y);
        }
        else if(slamming)
        {

            rb.velocity = new Vector2(0, -1 * slamsped);
        }
        else
        {
            rb.velocity = new Vector2(nextVelocityX, nextVelocityY);
        }

    }

    IEnumerator ActivateDash()
    {
     
        dashing = true;
        candash = false;
        yield return new WaitForSeconds(0.1f);
      
        dashing = false;
       
        yield return new WaitForSeconds(1);
   
        candash = true;
    }

    IEnumerator ActivateSlam()
    {
        print("test");
        slamming = true;
        canslam = false;
        yield return new WaitForSeconds(0.2f);
     
        slamming = false;

        yield return new WaitForSeconds(1);
      
        canslam = true;
    }

    IEnumerator tp(Vector3 pos)
    {
        cantp = false;
        em.CreateTPeffect(pos);
        yield return new WaitForSeconds(1);
        gameObject.transform.position = pos; 
        yield return new WaitForSeconds(2);
        cantp = true;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Breakable") && slamming)
        {
            Destroy(collision.gameObject);
        }
    }

}


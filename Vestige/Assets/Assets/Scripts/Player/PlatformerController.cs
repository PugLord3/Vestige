using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlatformController : MonoBehaviour
{
    #region vars
    private Rigidbody2D rb;
    private float horizontalInput;
    public GameObject yousuckthing;
    public EffectManager em;
    public GameObject currentCP;
    public LayerMask death;
    public bool tutorial;
    public bool candash = true;
    private bool cantp = true;
    public float moveSpeed = 10f;
    public float slamsped = 10f;
    public bool slamming = false;
    public bool canslam = true;
    public float dashspeed = 4f;
    public float jumpSpeed = 8f;
    public float teleportdistance = 100f;
    public Transform groundCheckPoint; // we really just need to get the position of an object, below the player, to determine where to check for the ground.
    public LayerMask groundLayer;
    private float groundCheckRadius = 0.2f;
    public bool dashing = false;
    public int maxJumps = 2;
    public int numJumps = 0;
    public int CheckPoint = 0;
    public LayerMask TutorialLayer;
    public static PlatformController instance;
    public float dashCD = 1f, slamCD = 1f, TPCD = 2f;
    #endregion


    private void Awake()
    {
        instance = this;
        currentCP = GameManager.instance.CurrentCheckPOINT;
        print(currentCP.name);
    }

    bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
    }
    bool tutcheck()
    {
        return Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, TutorialLayer);
    }
    bool tpcheck(Vector3 pos)
    {
        return Physics2D.OverlapCircle(pos, 0.05f, groundLayer);
    }
    bool deathcheck()
    {
        return Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, death);
    }

    void Start()
    {
        //because we are using physics, we need the rigidbody
        rb = GetComponent<Rigidbody2D>();
        transform.position = currentCP.transform.position;
      
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
            if (tutorial)
            {
                transform.position = currentCP.transform.position;
                print(currentCP.name + " Teleported");
            }
            else
            {
                SceneManager.LoadScene("Purgatory");
            }
        }
        if (tutcheck())
        {
            tutorial = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && numJumps > 0)
        {
            PlatformerAudioManager.instance.playSFX(PlatformerAudioManager.instance.jump);
            nextVelocityY = jumpSpeed;
            numJumps -= 1;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && cantp)
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.nearClipPlane));
            Vector2 place = (gameObject.transform.position - mouseWorldPosition).normalized;
            float x = place.x * teleportdistance;

            float y = place.y * teleportdistance;
            Vector2 spot = new Vector2(-x, -y);
            StartCoroutine(tp(spot));

           
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && candash)
        {
            print("1");
            PlatformerAudioManager.instance.playSFX(PlatformerAudioManager.instance.dash);
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
            Vector2 target = (gameObject.transform.position - mouseWorldPosition).normalized;
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
        UIManagerPlatformer.instance.startDash = true;

        dashing = false;
       
        yield return new WaitForSeconds(dashCD);
   
        candash = true;
    }

    IEnumerator ActivateSlam()
    {
        print("test");
        slamming = true;
        canslam = false;
        yield return new WaitForSeconds(0.5f);
        UIManagerPlatformer.instance.startSlam = true;

        slamming = false;

        yield return new WaitForSeconds(slamCD);
      
        canslam = true;
    }

    IEnumerator tp(Vector3 pos)
    {
        Vector3 place = gameObject.transform.position + pos;
        if (tpcheck(place) == false)
        {
            cantp = false;
            em.CreateTPeffect(gameObject.transform.position + pos);
            yield return new WaitForSeconds(1);
            gameObject.transform.position = place;

            UIManagerPlatformer.instance.startTP = true;
            PlatformerAudioManager.instance.playSFX(PlatformerAudioManager.instance.tp);
            yield return new WaitForSeconds(TPCD);
            cantp = true;
        }
        else
        {
            StartCoroutine(yourbad());
        }
    }

    IEnumerator yourbad()
    {
        yousuckthing.SetActive(true);
        yield return new WaitForSeconds(0.5F);
        yousuckthing.SetActive(false);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Breakable") && slamming)
        {
            Destroy(collision.gameObject);
        }
    }

}


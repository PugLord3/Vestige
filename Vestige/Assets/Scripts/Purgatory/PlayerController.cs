using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float speed = 5f;
    Rigidbody2D rb;
    public bool hasKey = false;
    public RoomHandler currentRoom;
    public float dashDuration = 0.1f, dashSpeed = 10f, currentDashTime = 0.1f, keepHealth, dashCooldown = 1f, dashCooldownTimer = 1f, slashCooldown = 1f, slashCooldownTimer = 0f;
    public int dashWindup = 50;
    public bool dashOngoing = false;
    private Vector2 dashdir;
    public GameObject claw, dashHitbox;

    void Start()    
    {
        dashHitbox.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !dashOngoing && (dashCooldownTimer <= 0f))
        {
            Thread.Sleep(dashWindup);
            currentDashTime = dashDuration;
            dashdir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Physics2D.IgnoreLayerCollision(10, 9); dashHitbox.SetActive(true);
            dashOngoing = true;
        }//start of dash
        else if (!dashOngoing)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            Vector2 inputDir = new Vector2(horizontalInput, verticalInput).normalized; // .normalized makes the vector have a magnitude of 1! it is important!

            rb.velocity = inputDir * speed;
        }//regular
        if(dashOngoing)
        {
            Dash();
        }

        if(dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        //claw

        if(Input.GetMouseButtonDown(0) && slashCooldownTimer <= 0)
        {
            Instantiate(claw, transform.position+(Vector3)ClawPos(), Quaternion.Euler(new Vector3(0,0,-ClawRot())));
            slashCooldownTimer = slashCooldown;
        }

        if(slashCooldownTimer > 0f)
        {
            slashCooldownTimer -= Time.deltaTime;
        }

    }

    void Dash()
    {
        currentDashTime -= Time.deltaTime;
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.nearClipPlane));
        Vector2 target = (gameObject.transform.position - mouseWorldPosition).normalized;
        float x = target.x * 50f;

        float y = target.y * 50f;
        rb.velocity = new Vector2(-x, -y);

        if (currentDashTime <= 0)
        {
            dashCooldownTimer = dashCooldown;
            dashOngoing = false;
            Physics2D.IgnoreLayerCollision(10, 9, false); dashHitbox.SetActive(false);
        }
    }

    Vector2 ClawPos()
    {
        Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 ret = mousepos - (Vector2)transform.position;
        return ret.normalized;
    }

    float ClawRot()
    {
        Vector2 mousepos = Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);

        Vector2 direction = mousepos - (Vector2)transform.position;
        return Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
    }

    private void OnDestroy()
    {
        SceneManager.LoadScene("Purgatory");
    }
}


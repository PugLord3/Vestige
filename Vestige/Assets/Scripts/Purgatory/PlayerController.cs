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
    public float dashDuration = 0.1f, dashSpeed = 10f, currentDashTime = 0.1f, keepHealth, dashCooldown = 1f, dashCooldownTimer = 1f;
    public int dashWindup = 50;
    public bool dashOngoing = false;
    private Vector2 dashdir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && !dashOngoing && (dashCooldownTimer <= 0f))
        {
            Thread.Sleep(dashWindup);
            currentDashTime = dashDuration;
            keepHealth = GetComponent<HP>().currentHP;
            GetComponent<HP>().currentHP = 99f;
            dashdir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
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

    }

    void Dash()
    {
        currentDashTime -= Time.deltaTime;
        rb.velocity = dashdir.magnitude > 0 ? dashdir * dashSpeed : new Vector2(dashSpeed, 0);

        if (currentDashTime <= 0)
        {
            dashCooldownTimer = dashCooldown;
            dashOngoing = false;
            PlayerController.instance.gameObject.GetComponent<HP>().currentHP = keepHealth;
        }
    }
}

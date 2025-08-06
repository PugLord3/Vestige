using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MoveHandler : MonoBehaviour
{
    public float dashDuration = 1f, speed = 2f, currentDashTime = 1f, keepHealth;
    public int dashWindup = 5;
    public bool dashOngoing = false;
    Rigidbody2D rb;
    private Vector2 dir;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos;

        if(Input.GetKeyDown(KeyCode.Q) && !dashOngoing)
        {
            Thread.Sleep(dashWindup);
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dir = mousePos-(Vector2)transform.position;
            currentDashTime = dashDuration;
            keepHealth = PlayerController.instance.gameObject.GetComponent<HP>().currentHP;
            PlayerController.instance.gameObject.GetComponent<HP>().currentHP = 99f;
            dashOngoing = true;
        }

        if(dashOngoing)
        {
            currentDashTime -= Time.deltaTime;
            rb.velocity = new Vector2(100, 100);

            if (currentDashTime <= 0)
            {
                dashOngoing = false;
                PlayerController.instance.gameObject.GetComponent<HP>().currentHP = keepHealth;
            }

        }
        
    }
}

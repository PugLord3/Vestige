using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 2f;
    public GameObject key;
    public bool hasKey = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance == null) return;
        Vector2 dir = (PlayerController.instance.transform.position-transform.position).normalized;
        rb.velocity = dir * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<HP>().takeDamage(1f);
        }
    }

    private void OnDestroy()
    {
        if(hasKey)
        {
            Instantiate(key,transform.position,key.transform.rotation);
        }
    }
}

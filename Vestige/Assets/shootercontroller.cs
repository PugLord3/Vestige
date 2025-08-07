using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class shooterhandler : EnemyHandler
{
    public float shotspeed = 2f;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    float timePassed = 0f;
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > 1f)
        {
            if (PlayerController.instance == null) return;
            Vector2 dir = (PlayerController.instance.transform.position - transform.position).normalized;
            print("Shot");
            GameObject goob = Instantiate(bullet, gameObject.transform);
            goob.GetComponent<Rigidbody2D>().velocity = dir * 3;
            StartCoroutine(killit(goob));
            timePassed = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<HP>().takeDamage(1f);
        }
    }

    IEnumerator killit(GameObject item)
    {
        yield return new WaitForSeconds(4);
        Destroy(item);
    }
}



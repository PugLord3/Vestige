using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 2f;
    public GameObject key;
    public bool dropsKey = false;

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



    private void OnDestroy()
    {
        if(dropsKey)
        {
            Instantiate(key,transform.position,key.transform.rotation);
        }
    }
}

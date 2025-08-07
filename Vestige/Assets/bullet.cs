using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float lifeTime = 3f;

    private void Update()
    {
        lifeTime -= Time.deltaTime;

        if(lifeTime <= 0 )
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<HP>().takeDamage(1f);
        }
    }

}

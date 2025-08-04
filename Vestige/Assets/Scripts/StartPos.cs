using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPos : MonoBehaviour
{
    public GameObject endPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PlayerController.instance.hasKey)
        {
            collision.transform.position = endPos.transform.position; //if player has key they tp
            PlayerController.instance.hasKey = false;
        }
    }
}

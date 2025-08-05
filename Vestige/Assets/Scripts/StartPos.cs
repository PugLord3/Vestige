using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class StartPos : MonoBehaviour
{
    public GameObject endPos;
    public float iframe, HPSTORE;
    public bool iframesOn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(iframesOn && iframe > 0)
        {
            iframe -= Time.deltaTime;
        }
        else if(iframesOn && iframe <= 0)
        {
            PlayerController.instance.gameObject.GetComponent<HP>().currentHP = HPSTORE;
            iframesOn = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PlayerController.instance.hasKey)
        {
            collision.transform.position = endPos.transform.position; //if player has key they tp
            PlayerController.instance.hasKey = false;
            HPSTORE = PlayerController.instance.gameObject.GetComponent<HP>().currentHP;
            PlayerController.instance.gameObject.GetComponent<HP>().currentHP = 9999;
            iframesOn = true;
            iframe = 0.5f;
        }
    }
}

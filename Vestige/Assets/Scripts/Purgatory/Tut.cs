using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public float timer = 5f;
    public GameObject tuti, tutt;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            tuti.SetActive(false);
            tutt.SetActive(false);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}

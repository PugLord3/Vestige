using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class BGMANAGER : MonoBehaviour
{
    public float length;
    private float startpos;
    public GameObject cam;
    public float parallaxEffect = 0.5f;
    void Start()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        startpos = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        float movement = cam.transform.position.x * (1 - parallaxEffect);
        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);
        if(movement  > startpos + length)
        {
            startpos += length;
        }
        else if(movement < startpos - length)
        {
            startpos -= length;
        }
    }
}

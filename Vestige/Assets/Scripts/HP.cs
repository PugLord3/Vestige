using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    public float currentHP, maxHP = 3f;
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float amt)
    {
        currentHP -= amt;

        if(currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TouchWords : MonoBehaviour
{
    public string texttodisplay;
    public float time;
    public GameObject plr;
    public TMP_Text text;
    public GameObject tutorialguy;
    private bool activated = true;

    IEnumerator textshow()
    {
     
        tutorialguy.SetActive(true);
        text.text = texttodisplay;
        yield return new WaitForSeconds(time);
  
        tutorialguy.SetActive(false);
        text.text = "";
        Destroy(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
    
        if (collision.gameObject == plr && activated)
        {
            activated = false;
          
            StartCoroutine(textshow());
            
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PurgatoryHandle : MonoBehaviour
{
    private int enemyCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Count();

        if(enemyCount == 1)
        {
            GameObject keyEnemy = GameObject.FindGameObjectWithTag("Enemy");
            keyEnemy.gameObject.GetComponent<EnemyHandler>().hasKey = true;
        }
    }
}

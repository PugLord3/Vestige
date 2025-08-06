using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PurgatoryHandle : MonoBehaviour
{
    public int enemyCount = 0;
    public static PurgatoryHandle instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Count();

        if(enemyCount == 1 && !PlayerController.instance.hasKey)
        {
            GameObject keyEnemy = GameObject.FindGameObjectWithTag("Enemy");
            keyEnemy.gameObject.GetComponent<EnemyHandler>().dropsKey = true;
        }
    }
}

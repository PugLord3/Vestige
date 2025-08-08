using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class PurgatoryHandle : MonoBehaviour
{
    public int enemyCount = 0;
    public static PurgatoryHandle instance;
    public bool playKeySound;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(8, 8);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Count();

   
        if(enemyCount == 0)
        {
            if(playKeySound)
            {
                PurgatoryAudioManager.instance.playSFX(PurgatoryAudioManager.instance.key);
                playKeySound = false;
            }

            PlayerController.instance.hasKey = true;
        }
        else
        {
            PlayerController.instance.hasKey = false;
            playKeySound = true;
        }
    }

    
}

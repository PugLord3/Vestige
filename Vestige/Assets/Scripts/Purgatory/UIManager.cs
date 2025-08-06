using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject key, nokey, allhearts, twohearts, oneheart, nohearts;
    public Image dashCooldown, slashCooldown;
    public TMP_Text EnemyCounter;

    public VertexGradient normal, oneleft;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        handleKey();
        handleHearts();
        handleEnemies();
        handleCooldowns();

    }
    
    void handleCooldowns()
    {
        //dash
        if(PlayerController.instance.dashCooldownTimer > 0)
        {
            dashCooldown.fillAmount = PlayerController.instance.dashCooldownTimer / PlayerController.instance.dashCooldown;
        } else
        {
            dashCooldown.fillAmount = 0;
        }

        //slash
        if (PlayerController.instance.slashCooldownTimer > 0)
        {
            slashCooldown.fillAmount = PlayerController.instance.slashCooldownTimer / PlayerController.instance.slashCooldown;
        }
        else
        {
            slashCooldown.fillAmount = 0;
        }
    }

    void handleKey()
    {
        if (PlayerController.instance.hasKey)
        {
            key.SetActive(true);
            nokey.SetActive(false);
        }
        else
        {
            key.SetActive(false);
            nokey.SetActive(true);
        }
    }

    void handleHearts()
    {
        if (PlayerController.instance == null)
        {
            allhearts.SetActive(false);
            twohearts.SetActive(false);
            oneheart.SetActive(false);
            nohearts.SetActive(true);
        }

        else if (PlayerController.instance.gameObject.GetComponent<HP>().currentHP == 3)
        {
            allhearts.SetActive(true);
            twohearts.SetActive(false);
            oneheart.SetActive(false);
            nohearts.SetActive(false);
        }
        else if (PlayerController.instance.gameObject.GetComponent<HP>().currentHP == 2)
        {
            allhearts.SetActive(false);
            twohearts.SetActive(true);
            oneheart.SetActive(false);
            nohearts.SetActive(false);
        }
        else if (PlayerController.instance.gameObject.GetComponent<HP>().currentHP == 1)
        {
            allhearts.SetActive(false);
            twohearts.SetActive(false);
            oneheart.SetActive(true);
            nohearts.SetActive(false);
        }
    }

    void handleEnemies()
    {
        switch(PurgatoryHandle.instance.enemyCount)
        {
            case 0:
                EnemyCounter.colorGradient = normal;
                EnemyCounter.text = "Proceed to the next room!";
                return;

            case 1:
                EnemyCounter.colorGradient = oneleft;
                break;

            default:
                EnemyCounter.colorGradient = normal;
                break;
                

        }//switch

        EnemyCounter.text = "Enemies left: " + PurgatoryHandle.instance.enemyCount;
    }
}

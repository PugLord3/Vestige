using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerPlatformer : MonoBehaviour
{
    public Image dashCD, slamCD, TPCD;
    public float dashTimer, slamTimer, TPTimer;
    public bool startDash, startSlam, startTP;
    public static UIManagerPlatformer instance;

    void Awake()
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
        handleCooldowns();

    }

    void handleCooldowns()
    {
        if (startDash)
        {
            dashTimer = PlatformController.instance.dashCD;
            startDash = false;
        }

        if (dashTimer > 0)
        {
            dashCD.fillAmount = dashTimer / PlatformController.instance.dashCD;
        } else
        {
            dashCD.fillAmount = 0;
        }

            dashTimer -= Time.deltaTime;

        //slam now

        if (startSlam)
        {
            slamTimer = PlatformController.instance.slamCD;
            startSlam = false;
        }

        if (slamTimer > 0)
        {
            slamCD.fillAmount = slamTimer / PlatformController.instance.slamCD;
        }
        else
        {
            slamCD.fillAmount = 0;
        }

        slamTimer -= Time.deltaTime;

        //tp now

        if (startTP)
        {
            TPTimer = PlatformController.instance.TPCD;
            startTP = false;
        }

        if (TPTimer > 0)
        {
            TPCD.fillAmount = TPTimer / PlatformController.instance.TPCD;
        }
        else
        {
            TPCD.fillAmount = 0;
        }

        TPTimer -= Time.deltaTime;
    }
}

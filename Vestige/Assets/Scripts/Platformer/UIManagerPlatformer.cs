using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerPlatformer : MonoBehaviour
{
    public Image dashCD;
    public float dashTimer;
    public bool onDashCD, startDash, onSlamCD;
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

        onDashCD = !(PlatformController.instance.candash && PlatformController.instance.dashing);

        if (onDashCD)
        {
            dashCD.fillAmount = dashTimer / PlatformController.instance.dashCD;
        } else
        {
            dashCD.fillAmount = 0;
        }

            dashTimer -= Time.deltaTime;
    }
}

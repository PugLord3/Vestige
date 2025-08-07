using System.Collections;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject gametext;
    public GameObject startbutton;

    void Start()
    {
        RectTransform rt=gametext.GetComponent<RectTransform>();
        Vector3 origscale=rt.localScale;
        Quaternion origrot=rt.localRotation;
        Vector2 targetpos=rt.anchoredPosition+new Vector2(0,100);
        rt.localScale=Vector3.zero;
        StartCoroutine(anim(rt,origscale,origrot,targetpos));
    }

    IEnumerator anim(RectTransform rt,Vector3 origscale,Quaternion origrot,Vector2 targetpos)
    {
        float t=0;
        while(t<2f)
        {
            t+=Time.deltaTime;
            float p=Mathf.SmoothStep(0,1,t/2f);
            rt.localScale=Vector3.Lerp(Vector3.zero,origscale,p);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        t=0;
        Vector2 startpos=rt.anchoredPosition;
        while(t<1.5f)
        {
            t+=Time.deltaTime;
            float p=Mathf.SmoothStep(0,1,t/1.5f);
            rt.anchoredPosition=Vector2.Lerp(startpos,targetpos,p);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        startbutton.SetActive(true);
        StartCoroutine(pulse(rt,origscale,origrot));
    }

    IEnumerator pulse(RectTransform rt,Vector3 origscale,Quaternion origrot)
    {
        float ramp=0;
        while(true)
        {
            ramp=Mathf.Min(1,ramp+Time.deltaTime*0.5f);
            float s=1+Mathf.Sin(Time.time*1.5f)*0.05f*ramp;
            rt.localScale=origscale*s;
            float r=Mathf.Sin(Time.time*1.5f)*5f*ramp;
            rt.localRotation=origrot*Quaternion.Euler(0,0,r);
            yield return null;
        }
    }
}

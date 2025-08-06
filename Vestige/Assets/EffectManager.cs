using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class EffectManager : MonoBehaviour
{
    public GameObject TPeffect;
    public void CreateTPeffect(Vector3 location)
    {
        StartCoroutine(spawneffectTP(location));
    }

    IEnumerator spawneffectTP(Vector3 location)
    {
    
        GameObject thin = Instantiate(TPeffect);
        thin.transform.position = location;
        yield return new WaitForSeconds(1);
        Destroy(thin);
    }
}

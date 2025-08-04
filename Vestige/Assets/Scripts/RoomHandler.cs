using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : MonoBehaviour
{
    public Vector2[] points;
    public GameObject enemyPrefab;
    public int enemyCount = 5;
    // Start is called before the first frame update
    void Start()
    {
        List<Vector2> list = new List<Vector2>();
        
        foreach(Transform childTransform in transform)
        {
            list.Add(new Vector2(childTransform.position.x,childTransform.position.y));
        }

        points = list.ToArray();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomHandler : MonoBehaviour
{
    public Vector2[] points;
    public List<GameObject> enemies = new List<GameObject>();
    public int enemyCount = 5;
    private PolygonCollider2D polygonCollider;
    // Start is called before the first frame update
    void Start()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
        List<Vector2> list = new List<Vector2>();
        
        foreach(Transform childTransform in transform)
        {
            list.Add(new Vector2(childTransform.position.x,childTransform.position.y));
        }

        points = list.ToArray();
        polygonCollider.points = points;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(0,0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bounds bounds = polygonCollider.bounds;

        if(collision.gameObject.tag == "Player")
        {
            print("Player has entered room");
            while(enemyCount > 0)
            {
                Vector2 randomPos = new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
                while(!polygonCollider.OverlapPoint(randomPos))
                {
                    randomPos = new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
                }
                GameObject enemyPrefab = enemies[Random.Range(0, enemies.Count)];
                print(enemies.Count);
                Instantiate(enemyPrefab, randomPos, enemyPrefab.transform.rotation);

                enemyCount--;
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    private BoxCollider2D boxCollider;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void SpawnRandomPrefab(int SpawnNum = 0)
    {
        float randomX = Random.Range(boxCollider.bounds.min.x, boxCollider.bounds.max.x);
        float randomY = Random.Range(boxCollider.bounds.min.y, boxCollider.bounds.max.y);

        Vector2 randomPosition = new Vector2(randomX, randomY);
        Instantiate(prefabs[SpawnNum], randomPosition, Quaternion.identity);
    }
}

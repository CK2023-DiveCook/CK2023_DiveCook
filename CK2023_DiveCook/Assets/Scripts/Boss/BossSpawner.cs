using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    private BoxCollider2D boxCollider;
    private int FishCount;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        FishCount = 0;
    }
    public void FishUpdate()
    {
        FishCount++;
        if(FishCount >= 50)
        {
            FishCount = 0;
            SpawnRandomPrefab(UnityEngine.Random.Range(0,3));
        }
    }
    public void SpawnRandomPrefab(int SpawnNum = 0)
    {
        float randomX = UnityEngine.Random.Range(boxCollider.bounds.min.x, boxCollider.bounds.max.x);
        float randomY = UnityEngine.Random.Range(boxCollider.bounds.min.y, boxCollider.bounds.max.y);

        Vector2 randomPosition = new Vector2(randomX, randomY);
        Instantiate(prefabs[SpawnNum], randomPosition, Quaternion.identity);
    }
}

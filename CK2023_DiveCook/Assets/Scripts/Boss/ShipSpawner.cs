using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    public GameObject Ship;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        SpawnPrefab();
    }
    private void SpawnPrefab()
    {
        float minX = boxCollider.bounds.min.x;
        float maxX = boxCollider.bounds.max.x;

        float randomX = Random.Range(minX, maxX);

        // �������� �ν��Ͻ�ȭ�մϴ�.
        Vector2 spawnPosition = new Vector2(randomX, boxCollider.transform.position.y);
        Instantiate(Ship, spawnPosition, Quaternion.identity);
    }
}

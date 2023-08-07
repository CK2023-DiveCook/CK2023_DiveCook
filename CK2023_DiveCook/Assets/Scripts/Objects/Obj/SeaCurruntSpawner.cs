using System;
using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SeaCurrentSpawner : MonoBehaviour
{
    [SerializeField] private float hXPos = -7.5f;
    [SerializeField] private Vector2 hYPos = new Vector2(0, -15);
    [SerializeField] private Vector2 vXPos = new Vector2(3.5f, 25.5f);
    [SerializeField] private float vYPos = 11;

    [SerializeField] private GameObject seaCurrent;
    [SerializeField] private float spawnTime = 7;
    [SerializeField] private float retryTime = 5;
    [SerializeField] private int spawnRate = 5;

    [SerializeField] private bool stop = false;
    private SeaCurrent _seaCurrent;

    private void Start()
    {
        _seaCurrent = seaCurrent.GetComponent<SeaCurrent>();
        StartCoroutine(nameof(SpawnSeaCurrent));
    }
    
    IEnumerator SpawnSeaCurrent()
    {
        seaCurrent.SetActive(false);
        while (!stop)
        {
            if (Random.Range(1, 100) > spawnRate)
            {
                yield return new WaitForSeconds(retryTime);
                continue;
            }
            seaCurrent.SetActive(true);
            var currentWay = Random.Range(0, 3);
            _seaCurrent.SetCurrentWay((CurrentWay)currentWay);
            if (currentWay < 2)
            {
                seaCurrent.transform.position = new Vector3(Random.Range(vXPos[0], vXPos[1]), vYPos);
            }
            else
            {
                seaCurrent.transform.position = new Vector3(hXPos, Random.Range(hYPos[0], hYPos[1]));
            }
            yield return new WaitForSeconds(spawnTime);
            seaCurrent.SetActive(false);
        }
    }
}

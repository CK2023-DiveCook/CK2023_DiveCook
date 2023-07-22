using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private ObjectPoolManager objectPoolManager;
    [SerializeField] private List<GameObject> spawnPoints;
    [SerializeField] private float spawnFrequency;
    [SerializeField] private bool stopSpawn;
    // Start is called before the first frame update
    void Start()
    {
        int cnt = spawnPoints.Count;
        for (int i = 0; i < cnt; i++)
        {
            StartCoroutine(SpawnFish(spawnPoints[i]));
        }
    }
    
    private GameObject GetFish()
    {
        var type = Random.Range(0, 3);
        return type switch
        {
            0 => objectPoolManager.GetFish(FishType.S),
            1 => objectPoolManager.GetFish(FishType.M),
            2 => objectPoolManager.GetFish(FishType.L),
            _ => null
        };
    }

    IEnumerator SpawnFish(GameObject sPoint)
    {
        yield return new WaitForSeconds(Random.value);
        while (true)
        {
            if (stopSpawn)
                break;
            GameObject fish = GetFish();
            if (fish)
            {
                fish.GetComponent<Fish>().spawnPos = sPoint.GetComponent<SpawnPoint>().spawnPos;
                fish.transform.position = sPoint.transform.position;
                fish.SetActive(true);
            }
            yield return new WaitForSeconds((float)(spawnFrequency + (Random.Range(-1, 1))));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class UrchinSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> urchinObjects;

    [SerializeField] private bool stopSpawnCycle = false;
    [SerializeField] private int setSpawnTime = 90;
    [SerializeField] private int ticTime = 1;
    private WaitForSeconds _tic;

    private void Start()
    {
        _tic = new WaitForSeconds(ticTime);
        for (var i = 0; i < urchinObjects.Count; i++)
        {
            urchinObjects[i].GetComponent<Fish>().Init(null, FishType.Urchin, 15);
            StartCoroutine(SpawnCycle(urchinObjects[i]));
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator SpawnCycle(GameObject urchinObject)
    {
        var spawnTime = setSpawnTime;
        var al = 0.1f;

        while (true)
        {
            if (stopSpawnCycle)
                break;
            yield return _tic;
            if (urchinObject.GetComponent<Fish>().isReady)
                continue;
            if (spawnTime <= 0)
            {
                al = 0.1f;
                spawnTime = setSpawnTime;
            }
            spawnTime -= 1;
            al += 0.01f;
            urchinObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, al);
            if (spawnTime <= 0)
            {
                urchinObject.GetComponent<Fish>().isReady = true;
                al = 1;
                urchinObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 0.8f, al);
            }
        }
    }
}

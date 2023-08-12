using System.Collections;
using System.Collections.Generic;
using Manager;
using Obj;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class FishSpawner : MonoBehaviour
{
	[SerializeField] private ObjectPoolManager objectPoolManager;
	[SerializeField] private List<GameObject> spawnPoints;
	[SerializeField] private float spawnFrequency;
	[SerializeField] private bool stopSpawn;

	[SerializeField] private List<int> sFishPercent = new List<int>(4);
	[SerializeField] private List<int> mFishPercent = new List<int>(4);
	[SerializeField] private List<int> lFishPercent = new List<int>(4);
	[SerializeField] private List<int> pFishPercent = new List<int>(4);
	[SerializeField] private List<int> sharkPercent = new List<int>(4);
	
	// Start is called before the first frame update
	void Start()
	{
		int cnt = spawnPoints.Count;
		for (int i = 0; i < cnt; i++)
		{
			StartCoroutine(SpawnFish(spawnPoints[i]));
		}
	}
	
	private GameObject GetFish(int level)
	{
		var sFishVal = sFishPercent[level];
		var mFishVal = sFishVal + mFishPercent[level];
		var lFishVal = mFishVal + lFishPercent[level];
		var pFishVal = lFishVal + pFishPercent[level];
		var sharkVal = pFishVal + sharkPercent[level];

		var rand = Random.Range(0, sharkVal);
		GameObject fish;
		
		if (rand <= sFishVal)
			fish = objectPoolManager.GetFish(FishType.Small);
		else if (rand <= mFishVal)
			fish = objectPoolManager.GetFish(FishType.Medium);
		else if (rand <= lFishVal)
			fish = objectPoolManager.GetFish(FishType.Large);
		else if (rand <= pFishVal)
			fish = objectPoolManager.GetFish(FishType.Puff);
		else
			fish = objectPoolManager.GetFish(FishType.Shark);
		return fish;
	}

	// ReSharper disable Unity.PerformanceAnalysis
	IEnumerator SpawnFish(GameObject sPoint)
	{
		yield return new WaitForSeconds(Random.value);
		while (true)
		{
			if (stopSpawn)
				break;
			GameObject fish = GetFish(sPoint.GetComponent<SpawnPoint>().spawnLevel);
			if (fish)
			{
				fish.GetComponent<Fish>().spawnPos = sPoint.GetComponent<SpawnPoint>().spawnPos;
				fish.transform.position = sPoint.transform.position + new Vector3(0, Random.Range(-1, 1), 0);
				if (sPoint.GetComponent<SpawnPoint>().spawnPos == SpawnPos.Left)
					fish.GetComponent<SpriteRenderer>().flipX = true;
				else
					fish.GetComponent<SpriteRenderer>().flipX = false;
				fish.SetActive(true);
			}
			yield return new WaitForSeconds((float)(spawnFrequency + (Random.Range(-1, 1))));
		}
	}
}

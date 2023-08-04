using System;
using System.Collections.Generic;
using Objects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Manager
{
	public enum FishType
	{
		None,
		Small,
		Medium,
		Large,
		Puff,
		Shark,
		Cnt
	}
	public class ObjectPoolManager : MonoBehaviour
	{
		[SerializeField] private GameObject sFish;
		[SerializeField] private GameObject mFish;
		[SerializeField] private GameObject lFish;
		[SerializeField] private GameObject pFish;
		[SerializeField] private GameObject shark;
		
		[SerializeField] private int poolSize = 20;
		
		[SerializeField] private List<GameObject> sFishPool;
		[SerializeField] private List<GameObject> mFishPool;
		[SerializeField] private List<GameObject> lFishPool;
		[SerializeField] private List<GameObject> pFishPool;
		[SerializeField] private List<GameObject> sharkPool;

		// Start is called before the first frame update
		private void Start()
		{
			sFishPool = new List<GameObject>();
			mFishPool = new List<GameObject>();
			lFishPool = new List<GameObject>();
			pFishPool = new List<GameObject>();
			sharkPool = new List<GameObject>();
			//리스트 생성
			InitPool(sFishPool, sFish, FishType.Small);
			InitPool(mFishPool, mFish, FishType.Medium);
			InitPool(lFishPool, lFish, FishType.Large);
			InitPool(pFishPool, pFish, FishType.Puff);
			InitPool(sharkPool, shark, FishType.Shark);
		}

		private void InitPool(List<GameObject>pool, GameObject prefab, FishType type)
		{
			for (int i = 0; i < poolSize; i++)
			{
				var newObj = Instantiate(prefab, this.transform, true);
				pool.Add(newObj);
				newObj.GetComponent<Fish>().Init(this, type);
				newObj.SetActive(false);
			}
		}

		private GameObject GetObjFromPool(List<GameObject>pool, GameObject prefab, FishType type)
		{
			if (pool.Count == 0)
			{
				var newObj = Instantiate(prefab, this.transform, true);
				newObj.GetComponent<Fish>().Init(this, type);
				return newObj;
			}
			GameObject returnObj = pool[0];
			pool.Remove(returnObj);
			return returnObj;
		}
		
		public GameObject GetFish(FishType type)
		{
			return type switch
			{
				FishType.Small => GetObjFromPool(sFishPool, sFish, type),
				FishType.Medium => GetObjFromPool(mFishPool, mFish, type),
				FishType.Large => GetObjFromPool(lFishPool, lFish, type),
				FishType.Puff => GetObjFromPool(pFishPool, pFish, type),
				FishType.Shark => GetObjFromPool(sharkPool, shark, type),
				_ => null
			};
		}
	
		public void ReturnFish(GameObject obj, FishType type)
		{
			obj.SetActive(false);
			switch (type)
			{
				case FishType.Small:
					sFishPool.Add(obj);
					break;
				case FishType.Medium:
					mFishPool.Add(obj);
					break;
				case FishType.Large:
					lFishPool.Add(obj);
					break;
				case FishType.Puff:
					pFishPool.Add(obj);
					break;
				case FishType.Shark:
					sharkPool.Add(obj);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, null);
			}
		}
	}
}

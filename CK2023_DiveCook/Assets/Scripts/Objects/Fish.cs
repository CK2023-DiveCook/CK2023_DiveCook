using System;
using Manager;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Fish : MonoBehaviour
{
	private Manager.ObjectPoolManager objectPoolManager;
	private new Rigidbody2D rigidbody2D;
		
	[SerializeField] private Manager.FishType fishType;
	[SerializeField] private float speed;
	[SerializeField] public SpawnPos spawnPos;
	[SerializeField] public bool isReady = false;

	private void Start()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
		isReady = false;
	}

	public void Init(Manager.ObjectPoolManager poolManager, Manager.FishType type)
	{
		fishType = type;
		if (fishType == FishType.Urchin)
			return;
		objectPoolManager = poolManager;
	}

	private void Update()
	{
		if (fishType == FishType.Urchin)
			return;
		rigidbody2D.velocity = spawnPos == SpawnPos.Right ? new Vector2(speed * -1, 0) : new Vector2(speed * 1, 0);
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (fishType == FishType.Urchin)
			return;
		if (col.transform.CompareTag("Despawn"))
		{
			objectPoolManager.ReturnFish(this.gameObject, fishType);
		}
	}
	public Manager.FishType Catch()
	{
		if (fishType == FishType.Urchin)
		{
			if (!isReady) return FishType.None;
			isReady = false;
			return fishType;
		}
		objectPoolManager.ReturnFish(this.gameObject, fishType);
		return fishType;
	}
}
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
	[SerializeField] public float oxygenDecrease = 0;
	[SerializeField] public SpawnPos spawnPos;
	[SerializeField] public bool isReady = false;

	private void Start()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
		isReady = false;
	}
	public void Init(Manager.ObjectPoolManager poolManager, Manager.FishType type, int oxygenDecreaseVal)
	{
		fishType = type;
		oxygenDecrease = oxygenDecreaseVal;
		if (fishType == FishType.Urchin)
			return;
		objectPoolManager = poolManager;
	}
	private void Update()
	{
		if (fishType == FishType.Urchin)
		{
			var r = transform.rotation;
			if (isReady)
				transform.Rotate(new Vector3(r.x, r.y,r.x + (3.3f * Time.deltaTime)));
			return;
		}
		rigidbody2D.velocity = spawnPos == SpawnPos.Right ? new Vector2(speed * -1, 0) : new Vector2(speed * 1, 0);
	}
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (fishType == FishType.Urchin)
			return;
		if (col.CompareTag("Despawn"))
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
	public static int GetScore(FishType fishType)
	{
		switch (fishType)
		{
			case FishType.Small:
				return 50;
			case FishType.Medium:
				return 100;
			case FishType.Large :
				return 200;
			case FishType.Puff:
				return 500;
			case FishType.Urchin:
				return 1200;
			case FishType.Shark:
				return 10000;
			case FishType.None:
				break;
			case FishType.Cnt:
			default:
				throw new ArgumentOutOfRangeException();
		}
		return 0;
	}
	
}
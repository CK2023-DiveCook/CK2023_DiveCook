using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Fish : MonoBehaviour
{
    private Manager.ObjectPoolManager objectPoolManager;
    private new Rigidbody2D rigidbody2D;
        
    [SerializeField] public Manager.FishType fishType;
    [SerializeField] public float speed;
    [SerializeField] public SpawnPos spawnPos;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Init(Manager.ObjectPoolManager poolManager, Manager.FishType type)
    {
        objectPoolManager = poolManager;
        fishType = type;
    }

    private void Update()
    {
        rigidbody2D.velocity = spawnPos == SpawnPos.Right ? new Vector2(speed * -1, 0) : new Vector2(speed * 1, 0);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Despawn"))
        {
            objectPoolManager.ReturnFish(this.gameObject, fishType);
        }
    }
}
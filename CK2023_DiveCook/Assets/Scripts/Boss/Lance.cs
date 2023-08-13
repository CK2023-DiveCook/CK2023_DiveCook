using Obj;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Lance : MonoBehaviour
{
    private int Damage = 40;
    private float Speed = 5;
    private Ship ship;
    private bool collisionCheck = false;

    public void FindShip(Ship S)
    {
        ship = S;
    }

    public void RotateUpdate(Transform target)
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void Fire(Transform target)
    {
        StartCoroutine(Shoot(target));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.transform.tag)
        {
            case "Fish":
                Fish fish = collision.GetComponent<Fish>();
                fish.Catch();
                collisionCheck = false;
                Delete();
                break;
            case "Despawn":
                collisionCheck = false;
                Delete();
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            Debug.Log("Player Ãæµ¹");
            PlayerOxygen ox = collision.transform.GetComponent<PlayerOxygen>();
            ox.AddOxygenLevel(-Damage);
            Delete();
        }
    }

    IEnumerator Shoot(Transform target)
    {
        Vector2 dir = (target.position - transform.position).normalized;
        yield return new WaitForSeconds(0.3f);
        while(!collisionCheck)
        {
            transform.position += (Vector3)dir * Speed * Time.deltaTime;
            yield return null;
        }
        yield break;
    }
    public void Delete()
    {
        ship.dieCheck = true;
        Destroy(gameObject);
    }

}

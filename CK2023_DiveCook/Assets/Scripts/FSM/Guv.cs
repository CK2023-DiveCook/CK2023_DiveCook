using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FSM;

public enum STATE
{
    IDLE,
    MOVE,
    ATTACK,
    RUSH
}

enum COOLTIME
{
    RUSH = 10,

}
public class Guv : MonoBehaviour
{
    private new Collider2D collider;

    public STATE currentState = STATE.IDLE;
    private Transform target;


    //Stat
    private string Name = "Guv";
    private float Hp = 900f;
    public float CurrentHp { get; private set; } = 900f;
    private float Damage = 70f;
    private float Speed = 1.0f;

    private void Start()
    {
        Hp = 900f;
        CurrentHp = Hp;
    }
    private void Update()
    {
        switch (currentState)
        {
            case STATE.IDLE:
                break;
            case STATE.MOVE:
                Vector2 direction = (target.position - transform.position).normalized;
                transform.position += new Vector3(direction.x, direction.y, 0) * Speed;
                break;
            case STATE.RUSH:
                break;
        }
    }
}

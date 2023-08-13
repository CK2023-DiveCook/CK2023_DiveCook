using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FSM;
using Manager;

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
    public Transform target;
    private SpriteRenderer spriteRenderer;


    //Stat
    private string Name = "Guv";
    private float Hp = 900f;
    public float CurrentHp { get; private set; } = 900f;
    public float Damage { get; private set; } = 70f;
    private float Speed = 3.0f;

    //Rush Data
    private float rushSpeed = 10f;
    private float rushArea = 10f;
    private float rushDistance = 10f;
    private bool rushCheck = true;
    [SerializeField] HpBar healthBar;

    private void Awake()
    {
        healthBar = GetComponentInChildren<HpBar>();
    }
    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        if (target == null)
        {
            Debug.Log("Boss Target NULL");
        }
        Hp = 900f;
        CurrentHp = Hp;
        healthBar.UpdateHealthBar(CurrentHp, Hp);
        currentState = STATE.MOVE;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Die()
    {
        GameManager.Instance.AddScore(10000);
        gameObject.SetActive(false);
    }

    public void TakeDamage(float damageAmount)
    {
        CurrentHp -= damageAmount;
        healthBar.UpdateHealthBar(CurrentHp, Hp);
        if(CurrentHp <= 0)
        {
            Die();
        }
    }

    public void ChangeState(STATE state)
    {
        currentState = state;
    }

    private void Update()
    {
        RushDirCheck();
        spriteFlip();
        switch (currentState)
        {
            case STATE.IDLE:
                break;
            case STATE.MOVE:
                Vector2 direction = (target.position - transform.position).normalized;
                transform.position += new Vector3(direction.x, direction.y, 0) * Speed * Time.deltaTime;
                break;
            case STATE.RUSH:
                if (rushCheck)
                {
                    rushCheck= false;
                    StartCoroutine(Rush());
                }
                break;
        }
    }
    private void spriteFlip()
    {
        if (target.position.x < transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else if (target.position.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("충돌 감지");
        if(collision.gameObject.CompareTag("Wall") && !rushCheck)
        {
            Debug.Log("벽 충돌 감지");
            StartCoroutine(WallStun());
        }
        else if(collision.gameObject.CompareTag("Net"))
        {
            TakeDamage(10f);
        }
        else if(collision.gameObject.CompareTag("Player"))
        {
            PlayerOxygen oxygen = collision.gameObject.GetComponent<PlayerOxygen>();
            oxygen.AddOxygenLevel(-Damage);
        }
    }


    private void RushDirCheck()
    {
        Collider2D area = Physics2D.OverlapCircle(transform.position, rushArea);
        if (area != null && area.CompareTag("Player") && rushCheck)  // rushCheck 값을 확인
        {
            ChangeState(STATE.RUSH);
        }
    }

    IEnumerator WallStun()
    {
        TakeDamage(250f);
        Debug.Log("1초 대기중");
        ChangeState(STATE.IDLE);
        yield return new WaitForSeconds(1f);
        Debug.Log("대기완료");
        ChangeState(STATE.MOVE);
        yield break;
    }

    IEnumerator RushCoolTime()
    {
        ChangeState(STATE.MOVE);
        yield return new WaitForSeconds((float)COOLTIME.RUSH);
        rushCheck = true;  // 쿨타임이 끝나면 rushCheck 값을 true로 다시 설정
        yield break;
    }

    IEnumerator Rush()
    {
        Vector2 dir = (target.position - transform.position).normalized;
        yield return new WaitForSeconds(1.5f);

        float moveDistance = 0f;

        while (moveDistance < rushDistance)
        {
            float Movdis = rushSpeed * Time.deltaTime;
            transform.position += (Vector3)dir * Movdis;

            moveDistance += Movdis;

            yield return null;
        }

        StartCoroutine(RushCoolTime());
        yield break;
    }
}
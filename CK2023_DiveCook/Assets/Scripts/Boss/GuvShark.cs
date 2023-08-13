using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class GuvShark : MonoBehaviour
{
    private new Collider2D collider;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    public Transform player;
    private Vector2 lastPlayerPosition;
    public string Name { get; private set; } = "Guv_Shark";
    private float Hp = 400f;
    public float CurrentHp { get; private set; } = 400f;
    public float Damage { get; private set; } = 80;
    private float Speed = 1.0f;
    private bool isMoving = true;

    // 크기 관련
    private float initialSizeX = 1f;
    private float initialSizeY = 1f;
    private float initialSpeed = 0.5f;
    private float sizeGrowthRate = 0.1f;  // 10% 증가율
    private float speedGrowthRate = 0.1f; // 10% 증가율

    private float sizeIncreaseRateX = (6f * 4.02f) / 10f * (1f / 6f); // 초당 가로 크기 증가율
    private float sizeIncreaseRateY = (6f * 4.02f) / 10f * (1f / 6f); // 초당 세로 크기 증가율
    private float speedIncreaseRate = 5.00f / 10f; // 초당 속도 증가율

    private float maxTime = 10f;

    private void Start()
    {
        InvokeRepeating("IncreaseSizeAndSpeed", 1f, 1f); // 1초 후부터 매 초마다 호출
        player = GameObject.FindWithTag("Player").transform;
        lastPlayerPosition = (player.position - transform.position).normalized; ;
        if (player == null )
        {
            Debug.Log("Boss Target NULL");
        }
        Hp = 400f;
        CurrentHp = Hp;
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void MoveTowardsPlayer()
    {
        float Movdis = initialSpeed* Time.deltaTime;
        transform.position += (Vector3)lastPlayerPosition * Movdis;
    }
    private void Update()
    {
        if (isMoving)
        {
            spriteFlip();
            MoveTowardsPlayer();
        }
    }
    private void spriteFlip()
    {
        if (player.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else if (player.position.x > transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("충돌 감지");
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("벽 충돌 감지");
            isMoving = false;
            Invoke("ResumeMovement", 0.5f);  // 0.5초 후에 움직임 재개
            lastPlayerPosition = player.position; // 벽에 부딪힐 때만 플레이어의 현재 위치를 갱신
        }
    }
    private void IncreaseSizeAndSpeed()
    {
        if (Time.timeSinceLevelLoad > maxTime)
        {
            CancelInvoke("IncreaseSizeAndSpeed");  // 10초 후에 크기 및 속도 증가 중지
            return;
        }

        // 현재 크기와 속도에 비례하여 증가
        initialSizeX += initialSizeX * sizeGrowthRate;
        initialSizeY += initialSizeY * sizeGrowthRate;
        initialSpeed += initialSpeed * speedGrowthRate;

        transform.localScale = new Vector3(initialSizeX, initialSizeY, 1f);

        // 콜라이더 크기 조절
        if (boxCollider)
        {
            float spriteWidth = spriteRenderer.sprite.bounds.size.x;
            float spriteHeight = spriteRenderer.sprite.bounds.size.y;

            boxCollider.size = new Vector2(spriteWidth * initialSizeX, spriteHeight * initialSizeY);
        }
    }
    /*private void IncreaseSizeAndSpeed()
    {
        if (Time.timeSinceLevelLoad > maxTime)
        {
            CancelInvoke("IncreaseSizeAndSpeed");  // 10초 후에 크기 및 속도 증가 중지
            return;
        }

        initialSizeX += sizeIncreaseRateX;
        initialSizeY += sizeIncreaseRateY;
        initialSpeed += speedIncreaseRate;   
        transform.localScale = new Vector3(initialSizeX, initialSizeY, 1f);
        boxCollider.size = new Vector2(initialSizeX, initialSizeY);
    }*/
    private void ResumeMovement()
    {
        isMoving = true;
    }

}

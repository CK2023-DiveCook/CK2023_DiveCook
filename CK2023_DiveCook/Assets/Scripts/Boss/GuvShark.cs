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

    // ũ�� ����
    private float initialSizeX = 1f;
    private float initialSizeY = 1f;
    private float initialSpeed = 0.5f;
    private float sizeGrowthRate = 0.1f;  // 10% ������
    private float speedGrowthRate = 0.1f; // 10% ������

    private float sizeIncreaseRateX = (6f * 4.02f) / 10f * (1f / 6f); // �ʴ� ���� ũ�� ������
    private float sizeIncreaseRateY = (6f * 4.02f) / 10f * (1f / 6f); // �ʴ� ���� ũ�� ������
    private float speedIncreaseRate = 5.00f / 10f; // �ʴ� �ӵ� ������

    private float maxTime = 10f;

    private void Start()
    {
        InvokeRepeating("IncreaseSizeAndSpeed", 1f, 1f); // 1�� �ĺ��� �� �ʸ��� ȣ��
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
        Debug.Log("�浹 ����");
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("�� �浹 ����");
            isMoving = false;
            Invoke("ResumeMovement", 0.5f);  // 0.5�� �Ŀ� ������ �簳
            lastPlayerPosition = player.position; // ���� �ε��� ���� �÷��̾��� ���� ��ġ�� ����
        }
    }
    private void IncreaseSizeAndSpeed()
    {
        if (Time.timeSinceLevelLoad > maxTime)
        {
            CancelInvoke("IncreaseSizeAndSpeed");  // 10�� �Ŀ� ũ�� �� �ӵ� ���� ����
            return;
        }

        // ���� ũ��� �ӵ��� ����Ͽ� ����
        initialSizeX += initialSizeX * sizeGrowthRate;
        initialSizeY += initialSizeY * sizeGrowthRate;
        initialSpeed += initialSpeed * speedGrowthRate;

        transform.localScale = new Vector3(initialSizeX, initialSizeY, 1f);

        // �ݶ��̴� ũ�� ����
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
            CancelInvoke("IncreaseSizeAndSpeed");  // 10�� �Ŀ� ũ�� �� �ӵ� ���� ����
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

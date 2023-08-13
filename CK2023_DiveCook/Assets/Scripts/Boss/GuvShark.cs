using Manager;
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
    private float startTime;
    public string Name { get; private set; } = "Guv_Shark";
    private float Hp = 400f;
    public float CurrentHp { get; private set; } = 400f;
    public float Damage { get; private set; } = 80;
    private float Speed = 1.0f;
    private bool isMoving = true;

    // ����ũ��
    private const float InitialTargetSizeX = 1f;
    private const float InitialTargetSizeY = 1f;
    private const float FinalTargetSizeX = 4f;
    private const float FinalTargetSizeY = 4f;
    private const float TargetSpeed = 5f;

    private float initialSpeed = 1f;

    [SerializeField] HpBar healthBar;

    private float maxTime = 10f;
    private void Awake()
    {
        healthBar = GetComponentInChildren<HpBar>();
    }
    private void Start()
    {
        startTime = Time.timeSinceLevelLoad;
        player = GameObject.FindWithTag("Player").transform;
        lastPlayerPosition = (player.position - transform.position).normalized; ;
        if (player == null )
        {
            Debug.Log("Boss Target NULL");
        }
        Hp = 400f;
        CurrentHp = Hp;
        healthBar.UpdateHealthBar(CurrentHp, Hp);
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteFlip();
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
        if (CurrentHp <= 0)
        {
            Die();
        }
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
            MoveTowardsPlayer();
        }
        IncreaseSizeAndSpeedSmoothly();
    }
    private void IncreaseSizeAndSpeedSmoothly()
    {
        float elapsedTime = Time.timeSinceLevelLoad - startTime;
        if (elapsedTime > maxTime) return;

        float progress = elapsedTime / maxTime; // 0���� 1������ �����

        // Lerp�� ����Ͽ� ���� ũ�� �� �ӵ� ���
        float newSizeX = Mathf.Lerp(InitialTargetSizeX, FinalTargetSizeX, progress);
        float newSizeY = Mathf.Lerp(InitialTargetSizeY, FinalTargetSizeY, progress);
        float newSpeed = Mathf.Lerp(0.5f, TargetSpeed, progress);

        transform.localScale = new Vector3(newSizeX, newSizeY, 1f);
        initialSpeed = newSpeed;

        // �ݶ��̴� ũ�� ����
        if (boxCollider)
        {
            boxCollider.size = spriteRenderer.sprite.bounds.size;
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
        Debug.Log("�浹 ������Ʈ : " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Wall"))
        {
            spriteFlip();
            isMoving = false;
            // ���� �ε��� �� �÷��̾��� ���� ��ġ���� ���� ���͸� ����Ͽ� ����
            lastPlayerPosition = (player.position - transform.position).normalized;
            Invoke("ResumeMovement", 0.5f);  // 0.5�� �Ŀ� ������ �簳
        }
        else if (collision.gameObject.CompareTag("Net"))
        {
            TakeDamage(10f);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            PlayerOxygen oxygen = collision.gameObject.GetComponent<PlayerOxygen>();
            oxygen.AddOxygenLevel(-Damage);
        }
    }
    private void ResumeMovement()
    {
        isMoving = true;
    }
}

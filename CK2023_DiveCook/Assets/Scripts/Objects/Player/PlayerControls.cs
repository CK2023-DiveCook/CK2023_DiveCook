using System;
using Manager;
using Objects;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
	[SerializeField] private Vector2 speed = new Vector2(50, 50);
	[SerializeField] private float gravity = -5;
		
	[SerializeField] private bool inCurrent = false;
	[SerializeField] private CurrentWay currentWay = CurrentWay.Null;
	[SerializeField] private float currentForce = 2.5f;
	private Rigidbody2D _rigidbody2D;
	private SpriteRenderer _spriteRenderer;

	private void Start()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_rigidbody2D.drag = 3;
	
		transform.Rotate(0,0,90);
		transform.Translate(new Vector3(0, -0.5f, 0), Space.World);
	}

	public void OnTriggerExit2D(Collider2D other)
	{
		if (!other.CompareTag("SeaCurrent")) return;
		inCurrent = false;
		currentWay = 0;
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag("SeaCurrent")) return;
		inCurrent = true;
		currentWay = other.GetComponent<SeaCurrent>().GetCurrentWay();
	}
	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.transform.CompareTag("Fish"))
		{
			var fishType = col.transform.GetComponent<Fish>().Catch();
			GetComponent<PlayerOxygen>().AddOxygenLevel(col.transform.GetComponent<Fish>().oxygenDecrease * -1);
			if (fishType is FishType.None or FishType.Shark)
				return;
			GameManager.Instance.AddScore(Fish.GetScore(fishType));
		}
		else if (col.transform.CompareTag("Bubble"))
		{
			GetComponent<PlayerOxygen>().AddOxygenLevel(GetComponent<PlayerOxygen>().oxygenIncrease);
			col.gameObject.SetActive(false);
		}
	}
	private void Move()
	{
		float deltaX = 0;
		float deltaY = 0;

		var inputX = Input.GetAxis("Horizontal");
		var inputY = Input.GetAxis("Vertical");

		deltaY = gravity;
		deltaX += inputX * speed.x;
		deltaY += inputY * speed.y;
		if (inCurrent)
		{
			switch (currentWay)
			{
				case CurrentWay.Up:
					deltaY += currentForce * 1;
					break;
				case CurrentWay.Down:
					deltaY += currentForce * -1;
					break;
				case CurrentWay.Left:
					deltaX += currentForce * -1;
					break;
				case CurrentWay.Right:
					deltaX += currentForce * 1;
					break;
				case CurrentWay.Null:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		_rigidbody2D.velocity = new Vector2(deltaX, deltaY);
	}
	private void Anim()
	{
		if (_rigidbody2D.velocity.x < -0.1)
				_spriteRenderer.flipY = false;
		else if (_rigidbody2D.velocity.x > 0.1)
				_spriteRenderer.flipY = true;
	}
	private void Update()
	{
		Move();
		Anim();
	}
}
using System;
using System.Collections;
using Manager;
using Objects;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
	[SerializeField] private Vector2 speed = new Vector2(50, 50);
	[SerializeField] private float gravityInWater = -5;
	[SerializeField] private float gravityOnLand = -10;
	[SerializeField] private float gravity = 0;
		
	[SerializeField] private bool isSwimming = false;
	[SerializeField] private bool inCurrent = false;
	[SerializeField] private CurrentWay currentWay = CurrentWay.Null;
	[SerializeField] private float currentForce = 2.5f;

	[SerializeField] private bool stopOxygenCycle = false;
	[SerializeField] private float oxygenLevel = 100;
	[SerializeField] private Slider oxygenLevelSlider;
	[SerializeField] private GameObject [] inventory = new GameObject[5];
	private int _inventoryIdx = 0;
		
	private SpriteRenderer _spriteRenderer;
	private Rigidbody2D _rigidbody2D;
	private WaitForSeconds _tic;

	void Start()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		gravity = gravityOnLand;
		_tic = new WaitForSeconds(1);
		StartCoroutine(OxygenCycle());
	}
	private IEnumerator OxygenCycle()
	{
		while (!stopOxygenCycle)
		{
			yield return _tic;
			AddOxygenLevel( -3.34f);
		}
	}
	public void AddOxygenLevel(float val)
	{
		if (oxygenLevel + val <= 0)
		{
			GameManager.Instance.GameOver();
			oxygenLevel = 0;
		}
		else
			oxygenLevel += val;
		oxygenLevelSlider.value = oxygenLevel;
	}
	public int GetInventoryScore()
	{
		int score = 0;
			
		for (int i = 0; i < 5; i++)
			score += inventory[i].GetComponent<FishBag>().GetScore();
		_inventoryIdx = 0;
		return score;
	}
	public void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Water"))
		{
			gravity = gravityOnLand;
			_rigidbody2D.drag = 0;
			isSwimming = false;
	
			transform.Rotate(0,0,-90);
			transform.Translate(new Vector3(0, 1f, 0),Space.World);
			//_rigidbody2D.velocity = new Vector2(0 , 50);
		}
		else if (other.CompareTag("SeaCurrent"))
		{
			inCurrent = false;
			currentWay = 0;
		}
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Water"))
		{
			gravity = gravityInWater;
			_rigidbody2D.drag = 3;
			isSwimming = true;
	
			transform.Rotate(0,0,90);
			transform.Translate(new Vector3(0, -0.5f, 0), Space.World);
		}
		else if (other.CompareTag("SeaCurrent"))
		{
			inCurrent = true;
			currentWay = other.GetComponent<SeaCurrent>().GetCurrentWay();
		}
	}
	private void OnCollisionEnter2D(Collision2D col)
	{
		Manager.FishType fishType;
			
		if (col.transform.CompareTag("Fish"))
		{
			fishType = col.transform.GetComponent<Fish>().Catch();
			if (_inventoryIdx >= 5)
				return;
			inventory[_inventoryIdx].GetComponent<FishBag>().SetImage(fishType);
			_inventoryIdx++;
		}
	}
	private void Move()
	{
		float deltaX = 0;
		float deltaY = 0;

		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

		deltaY = gravity;
		deltaX += inputX * speed.x;
		if (isSwimming)
			deltaY += inputY * speed.y;
		if (isSwimming && inCurrent)
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
		if (isSwimming)
		{
			_spriteRenderer.flipX = false;
			if (_rigidbody2D.velocity.x < -0.1)
				_spriteRenderer.flipY = false;
			else if (_rigidbody2D.velocity.x > 0.1)
				_spriteRenderer.flipY = true;
		}
		else
		{
			_spriteRenderer.flipY = false;
			if (_rigidbody2D.velocity.x < -0.2)
				_spriteRenderer.flipX = true;
			else if (_rigidbody2D.velocity.x > 0.2)
				_spriteRenderer.flipX = false;
		}
	}
	private void Update()
	{
		Move();
		Anim();
	}
}
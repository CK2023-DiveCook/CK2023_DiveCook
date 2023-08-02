using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeImage : MonoBehaviour
{
	public Sprite[] sprites;

	private SpriteRenderer spriteRenderer;
	private int currentIndex = 0;

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		if(sprites.Length > 0)
		{
			spriteRenderer.sprite = sprites[currentIndex];
		}
	}

	public void ChangeToNextSprite()
	{
		currentIndex = (currentIndex + 1) % sprites.Length;
		spriteRenderer.sprite = sprites[currentIndex];
	}
	public void FirstSprite()
	{
		currentIndex = 0;
		spriteRenderer.sprite = sprites[currentIndex];
	}
}

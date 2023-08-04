using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEngine.UI;

public class FishBag : MonoBehaviour
{
	[SerializeField] private Sprite fishEmpty;
	[SerializeField] private Sprite fishS;
	[SerializeField] private Sprite fishM;
	[SerializeField] private Sprite fishL;

	private Image _fishBagImage;
	private FishType _fishType;
	// Start is called before the first frame update
	void Start()
	{
		_fishBagImage = GetComponent<Image>();
	}

	public void SetImage(Manager.FishType fishType)
	{
		_fishType = fishType;
		switch (fishType)
		{
			case FishType.None :
				_fishBagImage.sprite = fishEmpty;
				return;
			case FishType.Large :
				_fishBagImage.sprite = fishL;
				return;
			case FishType.Medium :
				_fishBagImage.sprite = fishM;
				return;
			case FishType.Small :
				_fishBagImage.sprite = fishS;
				return;
			default:
				throw new ArgumentOutOfRangeException(nameof(fishType), fishType, null);
		}
	}
	public int GetScore()
	{
		int returnScore = 0;
		
		switch (_fishType)
		{
			case FishType.Large :
				returnScore = 200;
				break;
			case FishType.Medium:
				returnScore = 100;
				break;
			case FishType.Small:
				returnScore = 50;
				break;
			case FishType.None:
				returnScore = 0;
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
		_fishType = FishType.None;
		_fishBagImage.sprite = fishEmpty;
		return returnScore;
	}
}

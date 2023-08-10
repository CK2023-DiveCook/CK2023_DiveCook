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
	[SerializeField] private Sprite fishP;
	[SerializeField] private Sprite shark;
	[SerializeField] private Sprite urchin;

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
			case FishType.Puff:
				_fishBagImage.sprite = fishP;
				return;
			case FishType.Shark:
				_fishBagImage.sprite = shark;
				return;
			case FishType.Urchin:
				_fishBagImage.sprite = urchin;
				return;
			case FishType.Cnt:
			default:
				throw new ArgumentOutOfRangeException(nameof(fishType), fishType, null);
		}
	}
	public int GetScore()
	{
		int returnScore = 0;
		
		switch (_fishType)
		{
			case FishType.Small:
				returnScore = 50;
				break;
			case FishType.Medium:
				returnScore = 100;
				break;
			case FishType.Large :
				returnScore = 200;
				break;
			case FishType.Puff:
				returnScore = 500;
				break;
			case FishType.Urchin:
				returnScore = 1200;
				break;
			case FishType.Shark:
				returnScore = 10000;
				break;
			case FishType.None:
				break;
			case FishType.Cnt:
			default:
				throw new ArgumentOutOfRangeException();
		}
		_fishType = FishType.None;
		_fishBagImage.sprite = fishEmpty;
		return returnScore;
	}
}
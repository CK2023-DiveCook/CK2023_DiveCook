using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
	public class MiniGameManager : MonoBehaviour
	{
		private GameManager gamemanager;
		[SerializeField] ChangeImage Fish;
		[SerializeField] ChangeImage Gage;
		[SerializeField] GameObject SuccessImage;
		[SerializeField] GameObject FailImage;

		private int CuttingGage = 0;
		public Line[] Lines;
		private int CuttingNumber = 1;

		IEnumerator EndTimeCheck()
		{
			yield return new WaitForSeconds(3.0f);
			MiniGameLoader.Instance.EndCurrentScene();
		}

		private void Awake()
		{
			SuccessImage.SetActive(false);
			FailImage.SetActive(false);
			gamemanager = FindObjectOfType<GameManager>();
		}


		public void Success()
		{
			SuccessImage.SetActive(true);
			StartCoroutine(EndTimeCheck());
			gamemanager.CalScore();
		}
		public void Fail()
		{
			FailImage.SetActive(true);
			StartCoroutine(EndTimeCheck());
		}

		public void ChangeImage()
		{
			Fish.ChangeToNextSprite();
		}

		public int GetCuttingNumber()
		{
			return CuttingNumber;
		}

		public void CuttingNumberUp()
		{
			CuttingNumber++;
		}

		public void CuttingGageUp()
		{
			CuttingGage++;
			Gage.ChangeToNextSprite();
		}
		public int GetCuttingGage()
		{
			return CuttingGage;
		}
		public void CuttingGageReset()
		{
			CuttingGage = 0;
			Gage.FirstSprite();
		}
	}
}
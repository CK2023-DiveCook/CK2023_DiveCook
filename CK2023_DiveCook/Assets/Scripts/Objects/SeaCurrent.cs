using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
	public enum CurrentWay
	{
		Up,
		Down,
		Left,
		Right,
		Null
	}
	public class SeaCurrent : MonoBehaviour
	{
		[SerializeField] private CurrentWay currentWay;
		public CurrentWay GetCurrentWay()
		{
			return currentWay;
		}
		
		public void SetCurrentWay(CurrentWay way)
		{
			currentWay = way;
			switch (way)
			{
				case CurrentWay.Up:
					transform.rotation = Quaternion.Euler(0,0,-90);
					break;
				case CurrentWay.Down:
					transform.rotation = Quaternion.Euler(0,0,90);
					break;
				case CurrentWay.Left:
					transform.rotation = Quaternion.Euler(0,0, 0);
					break;
				case CurrentWay.Right:
					transform.rotation = Quaternion.Euler(0,0,180);
					break;
				case CurrentWay.Null:
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(way), way, null);
			}
		}
	}
}

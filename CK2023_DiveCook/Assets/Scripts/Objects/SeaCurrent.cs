using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaCurrent : MonoBehaviour
{
	[SerializeField] private int currentWay;
	public int GetCurrentWay()
	{
		return currentWay;
	}
}

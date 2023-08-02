using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnPos
{
	Right,
	Left
}
public class SpawnPoint : MonoBehaviour
{
	[SerializeField] public SpawnPos spawnPos;
}

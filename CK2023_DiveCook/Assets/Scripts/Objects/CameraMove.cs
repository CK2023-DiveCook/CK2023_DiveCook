using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
	[SerializeField] private float cameraSpeed = 5.0f;
	[SerializeField] private GameObject player;
	[SerializeField] Vector3 difValue; //플레이어와 캠과의 거리
	void Start()
	{
		difValue = transform.position - player.transform.position;
		difValue = new Vector3(0,0, difValue.z);
	}
	void FixedUpdate()
	{
		this.transform.position = Vector3.Lerp(this.transform.position, player.transform.position + difValue, cameraSpeed);
	}
}

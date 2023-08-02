using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
	private bool isPaused = false;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			TogglePause();
		}
	}

	public void TogglePause()
	{
		if (isPaused)
		{
			ResumeGame();
			Debug.Log("�Ͻ����� ����");
		}
		else
		{
			PauseGame();
			Debug.Log("�Ͻ�������");
		}
	}

	public void PauseGame()
	{
		Time.timeScale = 0f;
		isPaused = true;
	}

	public void ResumeGame()
	{
		Time.timeScale = 1f;
		isPaused = false;
	}
}

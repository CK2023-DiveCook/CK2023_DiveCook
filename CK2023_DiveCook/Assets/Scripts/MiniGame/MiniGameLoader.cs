using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameLoader : MonoBehaviour
{
	public static MiniGameLoader Instance { get; private set; }

	private int GameType;
	private string NowScene = " ";

	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void SetGameType(int i)
	{
		GameType = i;
	}

	public void LoadMiniGame()
	{
		switch(GameType)
		{
			case 0:
				NowScene = "MiniGame_type1";
				
				break;
			case 1:
				NowScene = "MiniGame_type2";
				break;			
		}
		SceneManager.LoadScene(NowScene, LoadSceneMode.Additive);
	}
	public void EndCurrentScene()
	{
		SceneManager.UnloadSceneAsync(NowScene);
		SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
	}
}

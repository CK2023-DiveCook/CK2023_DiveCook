using System;
using System.IO;
using Data;
using UnityEngine;

namespace Manager
{
	public class DataManager : Singleton<DataManager>
	{
		public AudioVolumeData AudioVolume = new AudioVolumeData();

		[SerializeField] private string audioDataFileName = "AudioData.json";

		private void Start()
		{
			LoadAudioData();
		}

		//음량 설정 저장하기
		public void SaveAudioData()
		{
			string toJsonData = JsonUtility.ToJson(AudioVolume, true);
			string filePath = Application.persistentDataPath + "/" + audioDataFileName;
			
			File.WriteAllText(filePath, toJsonData);
			
			Debug.Log("Saved AudioData at : <color=cyan>" + filePath + "</color>");
		}

		//음량 설정 불러오기
		public void LoadAudioData()
		{
			string filePath = Application.persistentDataPath + "/" + audioDataFileName;

			//저장된 게임이 있다면
			if (File.Exists(filePath))
			{
				string fromJsonData = File.ReadAllText(filePath);
				
				AudioVolume = JsonUtility.FromJson<AudioVolumeData>(fromJsonData);
				
				Debug.Log("Loaded AudioData");
			}
		}
	}
}

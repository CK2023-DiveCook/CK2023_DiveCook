using System.Collections;
using Objects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
	public class GameManager : Singleton<GameManager>
	{
		[SerializeField] private int score;
		[SerializeField] private float timeLimit = 60f;
		[SerializeField] private float ticTime = 0.1f;
		[SerializeField] private Slider timeLimitSlider;
		[SerializeField] private TextMeshProUGUI scoreTextMesh;
		[SerializeField] private PlayerControls player;
		private WaitForSeconds _tic;
		// Start is called before the first frame update
		private void Start()
		{
			_tic = new WaitForSeconds(ticTime);
			StartCoroutine(Timer());
		}
		private IEnumerator Timer()
		{
			while (true)
			{
				timeLimit -= 0.1f;
				timeLimitSlider.value = timeLimit;
				yield return _tic;
			}
			// ReSharper disable once IteratorNeverReturns
		}
		public void AddScore(int value)
		{
			score += value;
			scoreTextMesh.text = "SCORE : " + score.ToString();
		}
		public void CalScore()
		{
			score += player.GetInventoryScore();
			scoreTextMesh.text = "SCORE : " + score.ToString();
		}
		// ReSharper disable Unity.PerformanceAnalysis
		public void GameOver()
		{
			Debug.Log("GameOver");
		}
	}
}

using Manager;
using UnityEngine;

namespace Obj
{
	public class BigNet : MonoBehaviour
	{
		public void OnCollisionEnter2D(Collision2D col)
		{
			if (!col.transform.CompareTag("Fish")) return;
			var fishType = col.transform.GetComponent<Fish>().Catch();
			if (fishType is FishType.None)
				return;
			GameManager.Instance.AddScore(Fish.GetScore(fishType));
		}
	}
}

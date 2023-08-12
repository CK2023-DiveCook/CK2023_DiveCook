using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Obj
{
	public class Net : MonoBehaviour
	{
		[SerializeField] private float destroyTime = 10;
		[SerializeField] private GameObject bigNet;
		[SerializeField] private float speed;

		private PlayerControls player;
		private bool _stop = false;
		private Vector3 mousePosition;

		private void Start()
		{
			StartCoroutine(Destroyer(destroyTime));
		}

		public void SetRotate(Camera mainCamera, PlayerControls playerControls)
		{
			player = playerControls;
			mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
			var position = transform.position;
			transform.rotation = Quaternion.Euler(0,0,  Mathf.Atan2(mousePosition.y - position.y, mousePosition.x - position.x) * Mathf.Rad2Deg);
			Debug.Log(transform.rotation.z);
		}
		void Update()
		{
			if (_stop)
				GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
			else
				transform.Translate(Vector2.right * (speed * Time.deltaTime));
		}
		public void OnCollisionEnter2D(Collision2D col)
		{
			if (col.transform.CompareTag("Fish"))
			{
				if (!col.transform.CompareTag("Fish")) return;
				var fishType = col.transform.GetComponent<Fish>().Catch();
				if (fishType is FishType.None)
				{
					DoDestroy();
					return;
				}
				GameManager.Instance.AddScore(Fish.GetScore(fishType));
				player.PlaySound();
				bigNet.gameObject.SetActive(true);
				_stop = true;
				StartCoroutine(Destroyer(0.1f));
			}
			else if (col.transform.CompareTag("Fish"))
			{
				
			}
			DoDestroy();
		}
		private void DoDestroy()
		{
			this.gameObject.SetActive(false);
			Destroy(bigNet);
			Destroy(this.gameObject);
		}

		private IEnumerator Destroyer(float time)
		{
			yield return new WaitForSeconds(time);
			DoDestroy();
		}
	}
}

using System;
using System.Collections;
using Manager;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Obj
{
	public class Net : MonoBehaviour
	{
		[SerializeField] private float destroyTime = 10;
		[SerializeField] private GameObject bigNet;
		[SerializeField] private float speed = 1;
		private Camera _camera;
		private Rigidbody _rigidbody;

		private void Start()
		{
			_rigidbody = this.GetComponent<Rigidbody>();
			_camera = Camera.main;
			StartCoroutine(Destroyer(destroyTime));
		}

		// Update is called once per frame
		void Update()
		{
			Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
			Vector3 screenPoint = _camera.ScreenToWorldPoint(transform.position);
			Vector3 direction = (Input.mousePosition - screenPoint);
			direction.Normalize();
			_rigidbody.AddForce(direction * speed, ForceMode.Impulse);
			Vector3 dir;    
			if (Physics.Raycast(ray, out RaycastHit hit, 30f))
			{
				dir = hit.point - this.transform.position;
			}
			
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
				bigNet.gameObject.SetActive(true);
				StartCoroutine(Destroyer(0.1f));
			}
			else if (col.transform.CompareTag("Fish"))
			{
				
			}
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

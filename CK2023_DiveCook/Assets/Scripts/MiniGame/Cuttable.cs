using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuttable : MonoBehaviour
{
	public GameObject fracturedObject;

	public void Split()
	{
		// ���� ������Ʈ�� ��Ȱ��ȭ
		gameObject.SetActive(false);

		// �п��� ������Ʈ ����
		GameObject fractured = Instantiate(fracturedObject, transform.position, transform.rotation);
		fractured.SetActive(true);
	}
}
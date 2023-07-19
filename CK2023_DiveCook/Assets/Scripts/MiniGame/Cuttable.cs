using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuttable : MonoBehaviour
{
    public GameObject fracturedObject;

    public void Split()
    {
        // 원래 오브젝트를 비활성화
        gameObject.SetActive(false);

        // 분열된 오브젝트 생성
        GameObject fractured = Instantiate(fracturedObject, transform.position, transform.rotation);
        fractured.SetActive(true);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;
    [SerializeField] private GameObject target;
    private Vector3 _origin;

    // Start is called before the first frame update
    private void Start()
    {
        _origin = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        var position = target.transform.position;
        transform.position = new Vector3(_origin.x + (xOffset * position.x), _origin.y + (yOffset * position.y), _origin.z);
    }
}

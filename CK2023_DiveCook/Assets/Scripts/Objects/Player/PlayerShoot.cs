using System;
using Obj;
using UnityEngine;

namespace Player
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] int netCount = 5;
        [SerializeField] private GameObject netObject;
        private Camera _camera;
        private Vector2 _mouse;
        private Vector3 aim;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.LeftShift)) return;
            if (netCount <= 0) return;
            
            var bulletInstance = Instantiate(netObject);
            var position = transform.position;
            bulletInstance.transform.position = position;
        }
    }
}

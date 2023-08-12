using Obj;
using UnityEngine;

namespace Player
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private int netCount = 5;
        [SerializeField] private GameObject netObject;
        [SerializeField] private Camera mainCamera;
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Mouse0)) return;
            if (netCount <= 0) return;
            
            var bulletInstance = Instantiate(netObject);
            var position = transform.position;
            bulletInstance.transform.position = position;
            bulletInstance.GetComponent<Net>().SetRotate(mainCamera, GetComponent<PlayerControls>());
        }
    }
}

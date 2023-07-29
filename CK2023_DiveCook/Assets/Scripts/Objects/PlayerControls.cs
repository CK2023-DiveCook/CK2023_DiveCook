using UnityEngine;
using UnityEngine.Serialization;

namespace Objects
{
    public class PlayerControls : MonoBehaviour
    {
        [SerializeField] private Vector2 speed = new Vector2(50, 50);
        [SerializeField] private bool isSwimming = false;
        [SerializeField] private GameObject [] inventory = new GameObject[5];
        private int inventoryIdx = 0;
        private SpriteRenderer spriteRenderer;
        private new Rigidbody2D rigidbody2D;
    
        // Start is called before the first frame update
        void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Water"))
            {
                rigidbody2D.gravityScale = 20;
                rigidbody2D.drag = 0;
                isSwimming = false;
                transform.Rotate(0,0,-90);
                transform.Translate(new Vector3(0, 2f, 0),Space.World);
                //_rigidbody2D.velocity = new Vector2(0 , 50);
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Water"))
            {
                rigidbody2D.gravityScale = 0.5f;
                rigidbody2D.drag = 3;
                isSwimming = true;
                transform.Rotate(0,0,90);
                transform.Translate(new Vector3(0, -0.5f, 0), Space.World);
            }
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            Manager.FishType fishType;
            
            if (col.transform.CompareTag("Fish"))
            {
                fishType = col.transform.GetComponent<Fish>().Catch();
                inventory[inventoryIdx].GetComponent<FishBag>().SetImage(fishType);
                inventoryIdx++;
            }
        }
        
        private void Move()
        {
            Vector2 movement;
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");

            if ((inputX == 0 && inputY == 0))
                return;
            if (isSwimming)
                movement = new Vector2(speed.x * inputX, speed.y * inputY);
            else
                movement = new Vector2(speed.x * inputX, 0);
            rigidbody2D.velocity = movement;
        }
        private void Anim()
        {
            if (isSwimming)
            {
                spriteRenderer.flipX = false;
                if (rigidbody2D.velocity.x < -0.1)
                    spriteRenderer.flipY = false;
                else if (rigidbody2D.velocity.x > 0.1)
                    spriteRenderer.flipY = true;
            }
            else
            {
                spriteRenderer.flipY = false;
                if (rigidbody2D.velocity.x < -0.2)
                    spriteRenderer.flipX = true;
                else if (rigidbody2D.velocity.x > 0.2)
                    spriteRenderer.flipX = false;
            }
        }
        private void Update()
        {
            Move();
            Anim();
        }
    }
}

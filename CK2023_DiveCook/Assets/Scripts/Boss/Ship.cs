using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


enum ShipSTATE
{
    Charge,
    Fire,
    Move,
    IDLE
}


public class Ship : MonoBehaviour
{
    [SerializeField] Lance lance;
    [SerializeField] ShipSTATE state;
    [SerializeField] Transform target;
    private SpriteRenderer spriteRenderer;
    private float moveSpeed = 1f;
    public float fadeSpeed = 1.0f;
    public float FireTimeCheck = 0f;
    private bool MoveStart = false;
    public bool dieCheck { set; private get; } = false;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = GameObject.Find("Player").transform;
        dieCheck = false;
        FireTimeCheck = 0f;
        lance.FindShip(this);
        state = ShipSTATE.Charge;
        MoveStart = false;
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(fadeSpeed);
        MoveStart = true;
    }

    private void Update()
    {
        if(MoveStart)
        {
            if (dieCheck)
            {
                state = ShipSTATE.IDLE;
                if (spriteRenderer.color.a > 0)
                {
                    float newAlpha = spriteRenderer.color.a - fadeSpeed * Time.deltaTime;
                    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
                }
                else
                {
                    Destroy(gameObject);
                    gameObject.SetActive(false);
                }
            }
            switch (state)
            {
                case ShipSTATE.IDLE:
                    break;
                case ShipSTATE.Charge:
                    lance.RotateUpdate(target);
                    FireTimeCheck += Time.deltaTime;
                    if (FireTimeCheck >= 2.0f)
                    {
                        state = ShipSTATE.Fire;
                    }
                    break;
                case ShipSTATE.Fire:
                    lance.Fire(target);
                    state = ShipSTATE.IDLE;
                    break;
                    /*case ShipSTATE.Move:
                        float distanceX = target.position.x - transform.position.x;
                        float moveAmount = moveSpeed * Time.deltaTime;
                        if (Mathf.Abs(distanceX) > moveAmount)
                        {
                            transform.position += new Vector3(Mathf.Sign(distanceX) * moveAmount, 0, 0);
                        }
                        else
                        {
                            transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
                        }
                        break;*/
            }
        }
    }
        

}

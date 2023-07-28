using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] int Step = 0;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider= GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(boxCollider.OverlapPoint(mousePosition))
            {
                click();
            }
        }
    }

    public void click()
    {
        MiniGameManager manager = FindObjectOfType<MiniGameManager>();

        if(manager != null )
        {
            Debug.Log("line Click Error");
        }

        if(Step == manager.GetCuttingNumber())
        {
            manager.CuttingGageUp();
            /*if(manager.GetCuttingGage() == 6)
            {*/
                manager.ChangeImage();
                manager.CuttingNumberUp();
                /*manager.CuttingGageReset();*/
                gameObject.SetActive(false);
            /*}*/

        }


    }

    public int GetStep()
    {
        return Step;
    }
}

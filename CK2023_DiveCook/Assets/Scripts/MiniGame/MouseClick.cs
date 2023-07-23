using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    private int Count = 0;
    void Start()
    {
        Count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.name) 
        {
            case "Line":
                if(GetComponent<Line>().GetStep() == Count)
                {

                }
                break;
        }
    }
}

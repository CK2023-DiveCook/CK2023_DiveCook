using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = System.Numerics.Quaternion;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private Vector2 speed = new Vector2(50, 50);
    [SerializeField] private bool isSwimming = false;
    [SerializeField] private bool whileAnimation = false;
    private Rigidbody2D _rigidbody2D;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Move()
    {
        Vector2 movement;
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        if ((inputX == 0 && inputY == 0) || whileAnimation)
            return;
        if (isSwimming)
            movement = new Vector2(speed.x * inputX, speed.y * inputY);
        else
            movement = new Vector2(speed.x * inputX, 0);
        _rigidbody2D.velocity = movement;
    }

    private void ToLand()
    {
        whileAnimation = true;
    }
    
    private void ToWater()
    {
        whileAnimation = true;
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            _rigidbody2D.gravityScale = 1;
            _rigidbody2D.drag = 0;
            isSwimming = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            _rigidbody2D.gravityScale = 0.5f;
            _rigidbody2D.drag = 3;
            isSwimming = true;
            whileAnimation = false;
        }
        if (other.CompareTag("ToWater"))
        {
            _rigidbody2D.AddForce(new Vector2(50,-50));
        }
    }

    void Update()
    {
        Move();
    }
}

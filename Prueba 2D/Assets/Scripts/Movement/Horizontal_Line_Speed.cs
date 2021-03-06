﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horizontal_Line_Speed : Basic_Movement {

    public float distance = 4f;
    public float speed = 1f;
    public bool goLeft = false;
    public bool turnSpriteOnComple = true;

    private Vector3 destiny;
    private float currentDistance = 0;
    private int direction = 1;

    void Start()
    {

        rb2d = GetComponent<Rigidbody2D>();

        if (goLeft)
        {
            direction *= -1;
        }
        setDestinityBasedOnCurrentPosition();
        if (isSpriteLookingLeft ^ goLeft)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }

    void Update()
    {
        currentDistance = (destiny.x - transform.position.x) * direction;

        if (currentDistance <= 0 && allowMovement)
        {
            direction *= -1;
            if (turnSpriteOnComple)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            //Sets new destiny and adds the over distance to compensate
            destiny = new Vector3(direction * (distance + currentDistance * -1) + transform.position.x, transform.position.y);
            rb2d.velocity = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        if (allowMovement)
        {
            rb2d.AddForce(Vector2.right * speed * direction);

            //asigna min y max
            float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -speed, speed);

            rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);
        }
    }

    public void setDestiny(Vector2 newDestiny)
    {
        destiny = newDestiny;
    }

    public void setDestinityBasedOnCurrentPosition()
    {
        destiny = new Vector3(direction * distance + transform.position.x, transform.position.y);
    }

    public void disableAndResetWithDealy(float seconds)
    {
        setAllowMovementToFalse();
        Invoke("setDestinityBasedOnCurrentPosition", seconds);
        Invoke("setAllowMovementToTrue", seconds);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Movement : MonoBehaviour {

    protected bool allowMovement = true;
    protected Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public  void setAllowMovementToTrue()
    {
        allowMovement = true;
    }

    public void setAllowMovementToFalse()
    {
        allowMovement = false;
    }

    public void setSpeedToCero()
    {
        rb2d.velocity = Vector3.zero;
    }
}
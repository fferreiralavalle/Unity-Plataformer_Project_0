﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootOverTime : Basic_Enemy
{

    public GameObject bullet;
    public float timeBetweenBullets = 4f;
    public float InitialShootingDelay = 0;
    public Vector2 bulletSpeed = new Vector2(-1, 0);
    public Vector2 bulletOffSet = Vector2.zero;


    void Start()
    {
        startShooting();

    }

    public void shoot()
    {
        int spriteReversedMofifier = 1;
        if (GetComponent<Basic_Movement>() != null && GetComponent<Basic_Movement>().isSpriteLookingLeft)
        {
            spriteReversedMofifier = -1;
        }
            
        playSoundRandomized(attackSound);
        GameObject newBullet = Instantiate(bullet, null, true);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        newBullet.GetComponent<LinearInfinite>().speed = new Vector2
            (
            bulletSpeed.x * Mathf.Sign(transform.localScale.x),
            bulletSpeed.y
            );

        newBullet.transform.position = new Vector3
            (
            transform.position.x + bulletOffSet.x ,
            transform.position.y + bulletOffSet.y ,
            transform.position.z
            );
        newBullet.transform.localScale = new Vector3
            (
            newBullet.transform.localScale.x,
            Mathf.Abs(newBullet.transform.localScale.y) * Mathf.Sign(transform.localScale.x) * Mathf.Sign(bulletSpeed.x) ,
            newBullet.transform.localScale.z
            );
    }

    public void stopShooting()
    {
        CancelInvoke("shoot");
    }

    public void startShooting()
    {
        InvokeRepeating("shoot", timeBetweenBullets + InitialShootingDelay, timeBetweenBullets);
    }
}
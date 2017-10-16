using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Sky_Attacking_Controller : MonoBehaviour {

    public float trembleDistance = 0.5f;
    public float tremblePower = 1f;
    public float trembleIncrementPerFrame = 30f;
    public GameObject attackPrefab;

    private Animator anim;
    Horizontal_Line_Speed hls;
    GameObject bullet;
    private bool isShooting = false;

    void Start () {
        Invoke("playChargeAnim", 1.9f);
        anim = GetComponentInChildren<Animator>();
	}

    private void FixedUpdate()
    {
        if (hls != null && isShooting)
        {
            hls.speed += trembleIncrementPerFrame;
            bullet.transform.localScale = new Vector3(bullet.transform.localScale.x + 0.005f, bullet.transform.localScale.y + 0.005f, bullet.transform.localScale.z);
            bullet.GetComponent<Rotate>().cicleTime /= 1.05f;
        }
            
    }

    public void playChargeAnim()
    {
        anim.SetTrigger("Charge");
        gameObject.AddComponent(typeof(Horizontal_Line_Speed));
        hls = GetComponent<Horizontal_Line_Speed>();
        hls.speed = tremblePower;
        hls.distance = trembleDistance;
        hls.turnSpriteOnComple = false;
        Invoke("shoot", 1.5f);
        bullet = Instantiate(attackPrefab);
        bullet.transform.position = new Vector3 (transform.position.x, transform.position.y - 0.5f, transform.position.z);
        bullet.GetComponent<LinearInfinite>().speed = Vector2.zero;
        isShooting = true;
    }

    public void shoot()
    {
        anim.SetTrigger("Leave");
        isShooting = false;
        Invoke("destroySelf", 1f);
        bullet.GetComponent<LinearInfinite>().speed = new Vector2(0, -3);
    }

    public void destroySelf()
    {
        Destroy(gameObject);
    }
}

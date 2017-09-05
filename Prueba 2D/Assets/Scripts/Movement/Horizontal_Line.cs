using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horizontal_Line : MonoBehaviour {

    public float walkUntilTurnTime = 4f;
    public Vector2 speed = new Vector2(1, 0);
    public float waveCicleTime = 4f;
    public Vector2 waveIntensity = new Vector2(0,0);
    public bool isSpriteLookingLeft = false;

    private Rigidbody2D rb2d;
    private int direction = 1;
    private int waveDirection = 1;

    void Awake () {
        rb2d = GetComponent<Rigidbody2D>();
        InvokeRepeating("turnBack", walkUntilTurnTime, walkUntilTurnTime);
        InvokeRepeating("waveBack", waveCicleTime, waveCicleTime);
        if (isSpriteLookingLeft)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }
	
	void FixedUpdate () {
        rb2d.AddForce(speed * direction * 4);
        Vector2 limitedSpeed = new Vector2( 
            Mathf.Clamp(rb2d.velocity.x, -speed.x, speed.x),
            Mathf.Clamp(rb2d.velocity.y, -speed.y, speed.y)
            );
        Vector2 finalWaveIntensity = waveIntensity * waveDirection;
        rb2d.velocity = new Vector2(limitedSpeed.x + finalWaveIntensity.x, limitedSpeed.y + finalWaveIntensity.y);
    }

    public void turnBack()
    {
        direction *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        rb2d.velocity = Vector2.zero;
    }

    public void waveBack()
    {
        waveDirection *= -1;
    }
    }

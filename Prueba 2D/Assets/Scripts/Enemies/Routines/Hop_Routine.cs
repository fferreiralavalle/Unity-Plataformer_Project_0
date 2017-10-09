using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hop_Routine : Basic_Routine {

    public float hopPower = 14;

    private int direction = -1;

    private Basic_Movement movement;
    private CircleCollider2D cc2d;
    private Animator animator;
    private Rigidbody2D rb2d;

    // Update is called once per frame

    void Start()
    {
        InvokeRepeating("hop",0,4f);
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update () {
        
	}

    public void jump()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0); //reseteo Vel en y para evitar bug en plataforma medium
        rb2d.AddForce(Vector2.up * hopPower, ForceMode2D.Impulse);
        animator.SetTrigger("Jump");
    }
    public void hop()
    {
        jump();
        rb2d.AddForce(Vector2.right * hopPower * direction / 2, ForceMode2D.Impulse);
        //numberOfHops++;
    }
}

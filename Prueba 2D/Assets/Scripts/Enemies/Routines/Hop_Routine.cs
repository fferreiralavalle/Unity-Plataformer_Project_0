using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hop_Routine : Basic_Routine {

    public float hopPower = 14;
    public float timeBetweenHops = 2f;
    public float pauseBeforeHop = 1f; 
    private int direction = -1;

    private Basic_Movement movement;
    private CircleCollider2D cc2d;
    private Animator animator;
    private Rigidbody2D rb2d;

    // Update is called once per frame

    void Start()
    {
        InvokeRepeating("hop", timeBetweenHops, timeBetweenHops + pauseBeforeHop);
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }
    void Update () {
        
	}

    public void jump()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0); //reseteo Vel en y para evitar bug en plataforma medium
        rb2d.AddForce(Vector2.up * hopPower, ForceMode2D.Impulse);
        Basic_Movement bm = GetComponent<Basic_Movement>();
        if (bm != null)
            bm.setAllowMovementToTrue();
        animator.SetTrigger("Default");
        animator.SetTrigger("Jump");
        
    }

    public void hop()
    {
        StartCoroutine(hopRoutine());
    }
    private IEnumerator hopRoutine()
    {
        Invoke ("jump", pauseBeforeHop);
        Basic_Movement bm = GetComponent<Basic_Movement>();
        if (bm != null)
        {
            bm.setAllowMovementToFalse();
            bm.setSpeedToCero();
            GetComponent<Basic_Enemy>().playRecoverAnim();
            yield return new WaitForSeconds(0.8f);
            animator.SetTrigger("Idle");
        }
            
        //rb2d.AddForce(Vector2.right * hopPower * direction / 2, ForceMode2D.Impulse);
        //numberOfHops++;
    }
}

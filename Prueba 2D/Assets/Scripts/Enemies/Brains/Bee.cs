using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : ShootOverTime {

    private Basic_Movement movement;
    private Animator animator;
    private Renderer myRenderer;
    public Color reskinColor = new Color(0, 0, 0, 255);

    void Start () {
        startShooting();
        audioSource = GetComponent<AudioSource>();
        movement = GetComponent<Basic_Movement>();
        animator = GetComponentInChildren<Animator>();
        myRenderer = GetComponentInChildren<Renderer>();
        myRenderer.material.color = reskinColor;

    }
	
	void Update () {
        if (isDead)
            CancelInvoke("shoot");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        handlePlayerCollision(col);
    }

    public override void getStomped()
    {
        myRenderer.material.color = new Color32(255, 255, 255, 255);
        playSoundRandomized(hurtSound);
        setDeathToTrue();
        animator.SetTrigger("Die");
        Invoke("die", 1.3f);
        print("STOPMPEDEDED");
    }

    
}

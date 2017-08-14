using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour {

    public float health = 3;
    public float armor = 0;

    public float maxSpeed = 5f;
    public float speed = 20f;
    public float jumpPower = 9.25f;
    public int maxJumps = 2;
    public bool grounded;
    
    private int jumpNumber;

    private Rigidbody2D rb2d;
    private Animator anim;
    private bool keyJump;
    private bool doubleJump;
    private bool moveable = true;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        HUD_Manager.Instance.updateLifes(health);
        jumpNumber = 0;
    }

    // para inputs
    public void Update(){
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("Grounded", grounded);
        Debug.Log("Grounded" + grounded);

        if (grounded){
            jumpNumber = 0;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            Debug.Log("jump key");
            if (jumpNumber < maxJumps){
                Debug.Log("can jump");
                jumpNumber++;
                keyJump = true;
            }
        }
        if (transform.position.y < -30)
        {
            transform.position = Vector2.zero;
        }
    }

    //F I S I C A S  y  M O V I M I E N T O
    void FixedUpdate () {

        Vector2 fixedXVelocity = rb2d.velocity;
        fixedXVelocity.x *= 0.75f;

        if (grounded)
        {
            rb2d.velocity = fixedXVelocity;
        }

        float vh = Input.GetAxis("Horizontal");
        float dirh = Mathf.Sign(vh);

        if (!moveable) vh = 0;

        rb2d.AddForce(Vector2.right * speed * vh);
        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed); //asigna min y max
        rb2d.velocity = new Vector2 (limitedSpeed, rb2d.velocity.y);

        if (Mathf.Abs(vh)>0.1)
            transform.localScale = new Vector2 (dirh, transform.localScale.y);

        if (keyJump)
        {
           Debug.Log("jumped");
           jump();
        }

	}

    public void jump() {
        jump(jumpPower);
    }

    public void jump(float jumpPowerMod){
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0); //reseteo Vel en y para evitar bug en plataforma medium
        rb2d.AddForce(Vector2.up * jumpPowerMod, ForceMode2D.Impulse);
        keyJump = false;
    }

    public void push(Vector2 force){
        rb2d.AddForce(force, ForceMode2D.Impulse);
    }

    public void knockBack(float enemyPosX){
        float knockDirection = Mathf.Sign(transform.position.x - enemyPosX);
        Vector2 pushForce = new Vector2(knockDirection * jumpPower, 0);

        rb2d.velocity = Vector2.zero;
        jump(jumpPower/2);
        push(pushForce);
        disableMovement();
        Invoke("enableMovement", 0.5f);
    }

    public void enableMovement(){
        moveable = true;
    }

    public void disableMovement()
    {
        moveable = false;
    }

    public float takeDamage(float damage){
        animPlayRegularDamage();

        float trueDamageDealt = damage / (armor + 1);
        health -= trueDamageDealt;

        HUD_Manager.Instance.updateLifes(health);
        return trueDamageDealt;
    }

    public float takeFallDamage(float damage){
        health -= damage;
        HUD_Manager.Instance.updateLifes(health);
        return damage;
    }

    public void animPlayRegularDamage(){
        animPlayRegularDamage(3f);
    }

    public void animPlayRegularDamage(float seconds){
        anim.SetTrigger("TakeDamage");
    }
}

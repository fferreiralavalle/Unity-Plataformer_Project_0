using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour {

    public float initialHealth = 3;
    public float armor = 0;

    public float maxSpeed = 5f;
    public float speed = 20f;
    public float jumpPower = 9.25f;
    public float maxJumpPower = 18f;
    public float bounceJumpMultiplier = 1.5f;
    public int maxJumps = 2;
    public bool grounded;
    public AudioClip damagedSound;
    public AudioClip killedSound;
    public AudioClip secretKilledSound;
    public AudioClip fallToDeathSound;

    private int jumpNumber;

    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer sprite;

   
    private bool keyJump;
    private int initialJumpBugFixFrames = 5;
    private int currentJumpBugFixFrame  = 0;
    private bool moveable = true;
    private bool isDead = false;
    private bool hasFallen = false;
    private float health = 3;

    // Use this for initialization
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        HUD_Manager.Instance.updateLifes(health);
        jumpNumber = 0;
        health = initialHealth;

        DontDestroyOnLoad(gameObject);
    }

    // para inputs
    public void Update() {
        checkIfDead();
        if (!isDead)
        {
            anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
            anim.SetBool("Grounded", grounded);
            //Debug.Log("Grounded" + grounded);

            if (grounded && currentJumpBugFixFrame<=0)
            {
                jumpNumber = 0;
                currentJumpBugFixFrame = 0;
            }
            if (Input.GetButtonDown("Jump"))
            {
                if (jumpNumber < maxJumps)
                {
                    jumpNumber++;
                    Debug.Log("avalible jumps = "+ (maxJumps - jumpNumber));
                    keyJump = true;
                }
            }
            if (transform.position.y < -30)
            {
                transform.position = Vector2.zero;
            }
        }

    }

    //F I S I C A S  y  M O V I M I E N T O
    void FixedUpdate() {
        if (!isDead)
        {
            currentJumpBugFixFrame--;
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
            rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

            if (Mathf.Abs(vh) > 0.1)
                transform.localScale = new Vector3(dirh, transform.localScale.y, 1);

            if (keyJump)
            {
                jump();
            }
        }
    }

    public void jump() {
        jump(jumpPower);
    }

    public void bounceJump()
    {
        float newJumpPower = rb2d.velocity.y * bounceJumpMultiplier * -1;
        if (newJumpPower < jumpPower)
            newJumpPower = jumpPower;
        jump(newJumpPower);
        jumpNumber = 0;
    }

    public void jump(float jumpPowerMod) {
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0); //reseteo Vel en y para evitar bug en plataforma medium
        if (jumpPowerMod > maxJumpPower)
            jumpPowerMod = maxJumpPower;
        rb2d.AddForce(Vector2.up * jumpPowerMod, ForceMode2D.Impulse);
        keyJump = false;
        if (jumpNumber==1)
            currentJumpBugFixFrame = initialJumpBugFixFrames; // corrige bug de ground = true en primer salto
    }

    public void push(Vector2 force) {
        rb2d.AddForce(force, ForceMode2D.Impulse);
    }

    public void knockBack(float enemyPosX) {
        if (!isDead)
        {
            float knockDirection = Mathf.Sign(transform.position.x - enemyPosX);
            Vector2 pushForce = new Vector2(knockDirection * jumpPower, 0);

            rb2d.velocity = Vector2.zero;
            jump(jumpPower / 2);
            push(pushForce);
            disableMovement();
            Invoke("enableMovement", 0.5f);
        }

    }

    public void enableMovement() {
        moveable = true;
    }

    public void disableMovement()
    {
        moveable = false;
    }

    public float takeDamage(float damage) {
        if (!isDead)
        {
            animPlayRegularDamage();

            float trueDamageDealt = damage / (armor + 1);
            health -= trueDamageDealt;

            HUD_Manager.Instance.updateLifes(health);
            SoundManager.instance.PlaySingle(damagedSound);
            return trueDamageDealt;
        }
        return 0;
    }

    public float takeFallDamage(float damage) {
        health -= damage;
        HUD_Manager.Instance.updateLifes(health);
        return damage;
    }

    public void animPlayRegularDamage() {
        animPlayRegularDamage(3f);
    }

    public void animPlayRegularDamage(float seconds) {
        anim.SetTrigger("TakeDamage");
    }

    public void checkIfDead()
    {
        if (health <= 0 && !isDead)
        {
            isDead = true;
            float animationDelay = 0.2f;
            Invoke("playDeathAnimWithSound", animationDelay);
            GameMaster.Instance.handlePlayerDeath(1f + animationDelay); //kills player after x seconds
            sprite.color = new Color(1f, 1f, 1f, 0f);
        }
        else
        {
            if (!isDead)
                sprite.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    private void playDeathAnimWithSound()
    {
        rb2d.velocity = Vector2.zero;
        Invoke("playDeathSound", 0.08f);
        anim.SetTrigger("Killed");
    }

    private void playDeathSound()
    {
        if (Random.value > 0.20)
        {
            SoundManager.instance.PlaySingle(killedSound);
        }
        else
        {
            SoundManager.instance.PlaySingle(secretKilledSound);
        }
    }

    public void playDeadAnimation()
    {
        anim.SetTrigger("Dead");
    }
    
    public void revive()
    {
        isDead = false;
        health = initialHealth;
        anim.SetTrigger("Revive");
    }
    
    public void fallToDeath()
    {
        hasFallen = true;
        playFallingAnimAndSound();
        Invoke("setHasFallenToFalse", 1.5f);
    }

    public void setHasFallenToFalse()
    {
        hasFallen = false;
    }

    public void playFallingAnimAndSound()
    {
        SoundManager.instance.PlaySingle(fallToDeathSound);
    }

    public bool hasPlayerFell()
    {
        return hasFallen;
    }
}

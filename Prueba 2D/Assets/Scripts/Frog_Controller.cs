using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog_Controller : MonoBehaviour
{

    public string objectName = "Frog";

    public int maxHops = 3;
    public int minHops = 1;
    public float hopPower = 8f;
    public float timeBetweenHopsInSeconds = 3f;
    public float angerPerFlip = 5f;
    public float angerLostRatio = 0f;
    public AudioClip angrySound;

    private int numberOfHops = 0;
    private int direction = -1;
    private float currentAnger = 0;
    private const float maxAnger = 100;
    private bool isAngry = false;
    private bool playedAngrySound = false;
    private Color originalColor;

    private Rigidbody2D rb2d;
    private CircleCollider2D cc2d;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        cc2d = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = GetComponent<Renderer>().material.color;

        InvokeRepeating("hop", timeBetweenHopsInSeconds, timeBetweenHopsInSeconds);
        transform.localScale = new Vector3(direction * -1, transform.localScale.y, transform.localScale.z);
    }


    void Update()
    {

        float distance = 1f;
        Vector3 side = new Vector3(direction, 0, 0);
        // Debug Raycast in the editor.
        Vector3 rayPosition = new Vector3
            (
            transform.position.x,
            transform.position.y
            );

        Debug.DrawRay(rayPosition, side, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(rayPosition, side, distance);
        if (hit.collider != null)
        {
            print("froggy collided");
            turnAround();
        }
        GetComponent<Renderer>().material.color = new Color(
            originalColor.r + (10f * currentAnger / maxAnger),
            originalColor.g * (1f - currentAnger / maxAnger),
            originalColor.b * (1f - currentAnger / maxAnger),
            originalColor.a);
        if (!playedAngrySound && isAngry)
        {
            SoundManager.instance.RandomizeSfx(angrySound);
            playedAngrySound = true;
        }
    }

    void FixedUpdate()
    {
        currentAnger -= angerLostRatio;

        if (currentAnger < 0)
            currentAnger = 0;
        else if (currentAnger >= maxAnger)
        {
            isAngry = true;
            print("Frog is... ANGRYYYYYYYYYY");
        }
        else if (currentAnger < 100)
        {
            playedAngrySound = false;
            isAngry = false;
        }
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
        numberOfHops++;
    }

    public void turnAround()
    {
        direction *= -1;
        transform.localScale = new Vector3(direction * -1, transform.localScale.y, transform.localScale.z);
        numberOfHops = 0;
        currentAnger += angerPerFlip;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        print("frog hit something!");
        if (col.gameObject.tag == "Ground")
        {
            rb2d.velocity = new Vector3(0, rb2d.velocity.y);
            animator.SetTrigger("Idle");
            if (numberOfHops >= Mathf.FloorToInt(Random.Range(minHops, maxHops + 1)))
            {
                turnAround();
            }
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            print("frog hit Player :)");
            if (isAngry)
            {
                print("frog hit Player >:)");
                col.gameObject.GetComponent<Player_Controller>().takeDamage(2);
                col.gameObject.GetComponent<Player_Controller>().knockBack(transform.position.x);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Big_Slimeghter : Basic_Boss {

    public float hopPower = 10;

    public GameObject[] bossPlatforms;

    private int direction = -1;

    private Basic_Movement movement;
    private CircleCollider2D cc2d;
    private Animator animator;
    private Rigidbody2D rb2d;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = transform.Find("Sprite").GetComponent<Animator>();
        print("Component : " + movement);
        cc2d = GetComponent<CircleCollider2D>();
        movement = GetComponent<Basic_Movement>();
        rb2d = GetComponent<Rigidbody2D>();
        deactivatePlatforms();
        pickRoutine();
    }

    void Update () {

    }

    private void pickRoutine()
    {
        switch (routineNumber)
        {
            case 0:
                beginRoutine(0);
                break;
            default: break;
        }
    }
    

    

    void OnTriggerEnter2D(Collider2D col)
    {
        print("collided with " + col.gameObject.name);
        if (!isDead)
        {
            CircleCollider2D damageCollider = transform.Find("Damage_Collider").GetComponent<CircleCollider2D>();
            if (col.gameObject.tag == "Player")
            {
                Debug.Log("Found player");
                if ((transform.position.y + damageCollider.offset.y * 1.5f * transform.localScale.y) < col.transform.position.y)
                {
                    col.gameObject.GetComponent<Player_Controller>().bounceJump();
                    getStomped();
                }
                else if (!isRecoverying)
                {
                    col.gameObject.GetComponent<Player_Controller>().knockBack(transform.position.x);
                    col.gameObject.GetComponent<Player_Controller>().takeDamage(damageDealt);
                }
            }
        }
    }

    public void activatePlatforms()
    {
        foreach (GameObject g in bossPlatforms){
            g.SetActive(true);
        }
    }

    public void deactivatePlatforms()
    {
        foreach (GameObject g in bossPlatforms)
        {
            g.SetActive(false);
        }
    }

    public void getStomped()
    {
        takeDamage(1);
        endRoutine(0);
        animator.SetTrigger("Stomp");
        movement.setAllowMovementToFalse();
        movement.setSpeedToCero();
        setRecoveringToTrue();

        CancelInvoke("beginRecovering");
        CancelInvoke("finishRecovering");
        Invoke("beginRecovering", recoveryTimeInSeconds);
    }

    public void beginRecovering()
    {
        animator.SetTrigger("Recover");
        CancelInvoke("finishRecovering");
        Invoke("finishRecovering", animator.GetCurrentAnimatorClipInfo(0).Length);
        playSoundRandomizedWithDelay(recoverySound, 0.15f);
    }

    public void finishRecovering()
    {
        beginRoutine(0);
        animator.SetTrigger("Default");
        movement.setAllowMovementToTrue();
        setRecoveringToFalse();
    }
}

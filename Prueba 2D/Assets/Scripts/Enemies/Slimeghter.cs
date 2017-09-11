using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slimeghter : Basic_Enemy {

    private Basic_Movement movement;
    private bool isStomped = false;
    private CircleCollider2D cc2d;
    private Animator animator;

	void Start () {
        audioSource = GetComponent<AudioSource>();
        animator = transform.Find("Sprite").GetComponent<Animator>();
        print("Component : " + movement);
        cc2d = GetComponent<CircleCollider2D>();
        movement = GetComponent<Basic_Movement>();
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
                if ((transform.position.y + damageCollider.offset.y*1.5f) < col.transform.position.y)
                {
                    col.gameObject.GetComponent<Player_Controller>().bounceJump();
                    getStomped();
                }
                else if(!isRecoverying)
                {
                    col.gameObject.GetComponent<Player_Controller>().knockBack(transform.position.x);
                    col.gameObject.GetComponent<Player_Controller>().takeDamage(damageDealt);
                }
            }
        }
    }

    public void getStomped()
    {
        animator.SetTrigger("Stomp");
        playSoundRandomized(hurtSound);

        movement.setAllowMovementToFalse();
        movement.setSpeedToCero();
        setRecoveringToTrue();

        CancelInvoke("beginRecovering");
        CancelInvoke("finishRecovering");
        Invoke("beginRecovering",recoveryTimeInSeconds);
    }

    public void beginRecovering()
    {
        animator.SetTrigger("Recover");
        CancelInvoke("finishRecovering");
        Invoke("finishRecovering", animator.GetCurrentAnimatorClipInfo(0).Length);
        playSoundRandomizedWithDelay(recoverySound,0.15f);
    }

    public void finishRecovering()
    {
        animator.SetTrigger("Default");
        movement.setAllowMovementToTrue();
        setRecoveringToFalse();
    }


}

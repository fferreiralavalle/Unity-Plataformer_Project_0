using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Enemy : MonoBehaviour {

    public float damageDealt = 1;
    public float maxHealth = 1;
    public float recoveryTimeInSeconds = 4f;
    public AudioClip attackSound;
    public AudioClip hurtSound;
    public AudioClip recoverySound;

    protected bool isDead = false;
    protected bool isRecoverying = false;
    protected float currentHealth = 1;
    protected AudioSource audioSource;

    public void playDeathAnim()
    {
        if (GetComponent<Animator>() != null)
            GetComponent<Animator>().SetTrigger("Die");
        else if (GetComponentInChildren<Animator>() != null)
            GetComponentInChildren<Animator>().SetTrigger("Die");
    }

    public void playRecoverAnim()
    {
        if (GetComponent<Animator>()!=null)
            GetComponent<Animator>().SetTrigger("Recover");
        else if (GetComponentInChildren<Animator>() != null)
        {
            GetComponentInChildren<Animator>().SetTrigger("Recover");
        }
    }

    public void playAttackAnim()
    {
        if (GetComponent<Animator>() != null)
            GetComponent<Animator>().SetTrigger("Attack");
        else if (GetComponentInChildren<Animator>() != null)
            GetComponentInChildren<Animator>().SetTrigger("Attack");
    }

    public void playSoundRandomized(AudioClip clip)
    {
        float randomPitch = Random.Range(0.9f, 1.1f);

        audioSource.pitch = randomPitch;
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void playSoundRandomizedWithDelay(AudioClip clip, float delay)
    {
        float randomPitch = Random.Range(0.9f, 1.1f);

        audioSource.pitch = randomPitch;
        audioSource.clip = clip;
        audioSource.PlayDelayed(delay);
    }

    public void die()
    {
        Destroy(gameObject);
    }

    public void dieAfterXSeconds(float seconds)
    {
        Invoke("Die", seconds);
    }

    public bool isItDead()
    {
        return isDead;
    }

    public void setDeathToTrue()
    {
        isDead = true;
    }

    public void setDeathToFalse()
    {
        isDead = false;
    }

    public bool isItRecovering()
    {
        return isRecoverying;
    }

    public void setRecoveringToTrue()
    {
        isRecoverying = true;
    }

    public void setRecoveringToFalse()
    {
        isRecoverying = false;
    }
    
    public bool isStomped(Collider2D col)
    {
        CircleCollider2D damageCollider = transform.Find("Damage_Collider").GetComponent<CircleCollider2D>();
        return ( (transform.position.y + damageCollider.offset.y * 1.5f - damageCollider.radius * 0.5f) < col.transform.position.y );
    }

    public virtual void handlePlayerCollision(Collider2D col)
    {
        if (!isDead)
        {
            CircleCollider2D damageCollider = transform.Find("Damage_Collider").GetComponent<CircleCollider2D>();
            if (col.gameObject.tag == "Player")
            {
                Debug.Log("Found player");
                if (isStomped(col))
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

    public virtual void getStomped()
    {
        Destroy(gameObject);
    }

    public virtual void takeDamage(float damage)
    {
        currentHealth -= damage;
        playSoundRandomized(hurtSound);
        print("Entered regular take damage");
    }
}

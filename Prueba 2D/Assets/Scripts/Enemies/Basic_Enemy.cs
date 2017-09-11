using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Enemy : MonoBehaviour {

    public float damageDealt = 1;
    public float recoveryTimeInSeconds = 4f;
    public AudioClip attackSound;
    public AudioClip hurtSound;
    public AudioClip recoverySound;

    protected bool isDead = false;
    protected bool isRecoverying = false;
    protected AudioSource audioSource;

    public void playDeathAnim()
    {
        GetComponent<Animator>().SetTrigger("Die");
    }

    public void playRecoverAnim()
    {
        GetComponent<Animator>().SetTrigger("Recover");
    }

    public void playAttackAnim()
    {
        GetComponent<Animator>().SetTrigger("Attack");
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
    
}

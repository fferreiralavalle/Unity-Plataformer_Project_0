using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_simple_Spawn_Controller : Basic_Spawn {

    void Start()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", initialDelay + spawnTime, spawnTime);
        InvokeRepeating("spawnAnimation", initialDelay + spawnTime -1, spawnTime);
        audioSource = GetComponent<AudioSource>();
    }

    void spawnAnimation()
    {
        if (!isDisabled)
        {
            GameObject tuerca = transform.GetChild(0).gameObject;
            tuerca.GetComponent<Animator>().SetTrigger("Rotate");
            audioSource.clip = spawnSound;
            audioSource.PlayDelayed(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (isDisabled)
                return;
            isDisabled = true;
            audioSource.clip = spawnDownSound;
            audioSource.Play();
            GameObject tuerca = transform.GetChild(0).gameObject;
            tuerca.GetComponent<Animator>().SetTrigger("Disabling");
            Invoke("toggleLight", .3f);
            Invoke("toggleLight", .6f);
            Invoke("toggleLight", 1.54f);


        }
    }

    public void toggleLight()
    {
        GameObject light = transform.Find("Light").gameObject;
        light.SetActive(!light.activeSelf);
        Debug.Log("light spawn is now = " + light.activeSelf);
    }
}

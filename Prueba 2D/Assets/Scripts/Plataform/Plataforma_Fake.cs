using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma_Fake : MonoBehaviour {

    public AudioClip woopsSound;
    public float sideMovement = 1f;
    public float timeBetweenMovement = 3f;
    public float sideToSideTime = 1f;

    private Vector3 originalPosition;
    
    private int direction = 1;
    private bool canShake = true;

    void Awake()
    {
        originalPosition = transform.localPosition;
        Debug.Log("original pos x " + originalPosition.x);
        
    }

    public void activateShake()
    {
        canShake = true;
    }

    private void FixedUpdate()
    {
        if (!canShake)
            return;

        Vector3 destiny = new Vector3(originalPosition.x + sideMovement*direction, transform.localPosition.y);

        float fixedTime = Time.deltaTime / sideToSideTime; //2 -> half to one side, half to the other
        if (direction == 1)
            fixedTime /= 2;

        Vector2 newPosition = Vector2.MoveTowards(transform.localPosition, destiny, fixedTime);
        float distance = newPosition.x - transform.localPosition.x;
        transform.localPosition = newPosition;

        if (transform.localPosition == destiny)
        {
            direction *= -1;
        }

        // if its coming back and is in the middle then it finished the cicle
        if (
            (direction == 1) && 
            transform.localPosition.x >= (originalPosition.x - distance) &&
            transform.localPosition.x <  originalPosition.x
            )
        {
            Debug.Log("stoping shaking :c");
            canShake = false;
            Invoke("activateShake", timeBetweenMovement);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            SoundManager.instance.RandomizeSfx(woopsSound);
            canShake = true;
            for (int i = 0; i < transform.childCount; i++) {
                GameObject plataformPart = transform.GetChild(i).gameObject;
                plataformPart.GetComponent<SpriteRenderer>().color = new Color(150,0,0);
             }
        }
    }
}

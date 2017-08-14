using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaFalling : MonoBehaviour {

    public float fallDelay = 1f;
    public float respawnDelay = 5f;

    private Rigidbody2D rb2d;
    private PolygonCollider2D pc2d;
    private Vector2 start;
    private Animator animator;
    private bool falling;

    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        pc2d = GetComponent<PolygonCollider2D>();
        animator = GetComponent<Animator>();
        start = transform.position;
        falling = false;
    }
	

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !falling)
        {
            falling = true; //evita que se llame varias veces antes de caer
            Invoke("fall", fallDelay);
            Invoke("respawn", fallDelay + respawnDelay);
        }
    }

    void fall(){
        rb2d.isKinematic = false; //Lo hace dinamico y que por lo tanto lo afecte la gravedad.
        pc2d.isTrigger = true; //evita que le afecten las colisiones.
    }

    void respawn(){
        transform.position = start;
        rb2d.isKinematic = true; 
        pc2d.isTrigger = false;
        falling = false;
        rb2d.velocity = Vector2.zero;
        animator.SetTrigger("appear");
    }
}

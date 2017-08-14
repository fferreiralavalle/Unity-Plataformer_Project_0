using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour {

    public float maxSpeed = 5f;
    public float speed = 20f;
    public float damageDealt = 1f;

    private Rigidbody2D rb2d;
    private CircleCollider2D cc2d;

    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        cc2d = GetComponent<CircleCollider2D>();
    }
	
	
	void FixedUpdate () {
        rb2d.AddForce(Vector2.right * speed);

        //asigna min y max
        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);

        //forma para fijarse si choco con algo solido
        //Debug.Log("turn around? : "+(limitedSpeed > -0.05f && limitedSpeed < 0.05f));
        if (limitedSpeed > -0.01f && limitedSpeed < 0.01f){
            limitedSpeed = speed *= -1; //se lee de der a izq
        }
        //Debug.Log("Speed = "+limitedSpeed);
        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

        animationDirection();
    }

    void animationDirection(){
        float dirX = rb2d.velocity.x;
        if (dirX > 0){
            transform.localScale = new Vector2(-1f,transform.localScale.y);
        }else if (dirX < 0){
            transform.localScale = new Vector2(1f, transform.localScale.y);
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == "Player"){
            Debug.Log("Found player");
            if ( (transform.position.y + cc2d.radius/2) < col.transform.position.y){
                col.gameObject.GetComponent<Player_Controller>().jump();
                Destroy(gameObject);
            }else{
                col.gameObject.GetComponent<Player_Controller>().knockBack(transform.position.x);
                col.gameObject.GetComponent<Player_Controller>().takeDamage(damageDealt);
            }
        }
    }
}

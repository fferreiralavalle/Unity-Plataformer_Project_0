using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkGround : MonoBehaviour {

    private Player_Controller player;
    private Rigidbody2D rb2d;
    
	// Use this for initialization
	void Start () {
        player = GetComponentInParent<Player_Controller>();
        rb2d = GetComponentInParent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Platform")
        {
            rb2d.position = Vector2.zero;
            player.grounded = true;
            player.transform.parent = col.transform;
        }
    }

    private void OnCollisionStay2D(Collision2D col){
        //Debug.Log("colission!");
        if (col.gameObject.tag == "Ground"){
            player.grounded = true;
        }
        if (col.gameObject.tag == "Platform")
        {
            player.grounded = true;
            player.transform.parent = col.transform;
        }

    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground"){
            player.grounded = false;
        }
        if (col.gameObject.tag == "Platform")
        {
            player.grounded = false;
            player.transform.parent = null;
        }

    }

}

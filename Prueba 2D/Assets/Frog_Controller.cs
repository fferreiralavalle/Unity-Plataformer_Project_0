using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog_Controller : MonoBehaviour {

    public int maxHops = 3;
    public int minHops = 1;
    public float hopPower = 8f;
    public float timeBetweenHopsInSeconds = 3f;

    private int numberOfHops = 0;
    private int direction = 1;

    private Rigidbody2D rb2d;
    private CircleCollider2D cc2d;

    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        cc2d = GetComponent<CircleCollider2D>();
        InvokeRepeating("jump", timeBetweenHopsInSeconds, timeBetweenHopsInSeconds);
    }
	
	
	void Update () {
        RaycastHit hit;
        float distance;
        // Debug Raycast in the editor.
        Vector3 side = transform.TransformDirection(Vector3.right) * 10 * -1;
        Debug.DrawRay(transform.position, side, Color.green);

        if (Physics.Raycast(transform.position, side, out hit))
        {
            distance = hit.distance;
            print(distance + " is the distance");
        }
    }

    public void jump()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0); //reseteo Vel en y para evitar bug en plataforma medium
        rb2d.AddForce(Vector2.up * hopPower, ForceMode2D.Impulse);
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        print("frog hit something!");
    }

}

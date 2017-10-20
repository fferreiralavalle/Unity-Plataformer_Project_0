using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Speed_Seconds : MonoBehaviour {

    public float seconds = 4f;
    public Vector2 speed = new Vector2(1, 0);

    void Start () {
        Invoke("selfDestroy", 0.5f);
	}

    public void selfDestroy()
    { 
        Destroy(this, seconds - 0.5f);
    }
	
	void FixedUpdate () {
        transform.position = new Vector3(transform.position.x + speed.x * Time.deltaTime, transform.position.y + speed.y * Time.deltaTime, transform.position.z) ;
	}


}

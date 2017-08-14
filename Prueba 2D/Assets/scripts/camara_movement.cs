using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camara_movement : MonoBehaviour {

    public GameObject target;
    public Vector2 minCamPos, maxCamPos;
    public float smoothTime;
    public float distanceZ;

    private Vector2 velocity;

    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float posX = Mathf.SmoothDamp(
            transform.position.x,
            target.transform.position.x,
            ref velocity.x,
            smoothTime
            );
        float posY = Mathf.SmoothDamp(
            transform.position.y,
            target.transform.position.y,
            ref velocity.y,
            smoothTime
            );

        transform.position = new Vector3(
            posX,
            posY + (distanceZ*.25f),
            -distanceZ
            );
	}
}

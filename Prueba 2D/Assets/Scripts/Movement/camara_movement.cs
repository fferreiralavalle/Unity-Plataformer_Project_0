using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camara_movement : MonoBehaviour {

    public GameObject target = null;
    public Vector2 minCamPos, maxCamPos;
    public float yOffSet = 0.3f;
    public float smoothTime;

    private Vector2 velocity;
    private bool hasFallen = false;

    void Start () {
		if (target == null)
        {
            target = GameMaster.Instance.getPlayer();
        }

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (target.tag == "Player")
            hasFallen = target.GetComponent<Player_Controller>().hasPlayerFell();
        if (!hasFallen)
        {
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
                posY + yOffSet,
                -10
                );
        }
        
	}

}

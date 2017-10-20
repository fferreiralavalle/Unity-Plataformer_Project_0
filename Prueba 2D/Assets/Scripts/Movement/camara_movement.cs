using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camara_movement : MonoBehaviour {

    public GameObject target = null;
    public Vector2 minCamPos, maxCamPos;
    public float yOffSet = 0.3f;
    public float smoothTime;
    public bool canMove = true;

    private Vector2 velocity;
    private bool hasFallen = false;
    private float initialYOffset;

    private bool changingYOffset = false;
    private float newYOffset = 0.5f;

    private bool changingSize = false;
    private float newSize = 5f;

    private float velAux = 0.1f;

    void Start () {
        initialYOffset = yOffSet;
        if (target == null)
        {
            target = GameMaster.Instance.getPlayer();
        }

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (target.tag == "Player")
            hasFallen = target.GetComponent<Player_Controller>().hasPlayerFell();


        if (changingYOffset)
        {
            yOffSet = Mathf.SmoothDamp(
                yOffSet,
                newYOffset,
                ref velAux,
                smoothTime * 3
                );
        }
        print("VEL AUX = " + velAux);
        if (!hasFallen && canMove)
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
        
        if (changingSize)
        {
            GetComponent<Camera>().orthographicSize += 0.5f * Time.deltaTime * Mathf.Sign(newSize);
            float currentDistance = (newSize - GetComponent<Camera>().orthographicSize) * Mathf.Sign(newYOffset);
            print("current Camera size Distance = " + currentDistance);
            if (currentDistance < 0)
            {
                GetComponent<Camera>().orthographicSize = newSize;
                changingSize = false;
            }
        }


    }

    public void changeYOffset(float newOffset)
    {
        changingYOffset = true;
        newYOffset = newOffset;
    }

    public void changeSize(float newCamaraSize)
    {
        changingSize = true;
        newSize = newCamaraSize;
    }

    public void changeYOffsetToIntial()
    {
        changingYOffset = true;
        newYOffset = initialYOffset;
    }

}

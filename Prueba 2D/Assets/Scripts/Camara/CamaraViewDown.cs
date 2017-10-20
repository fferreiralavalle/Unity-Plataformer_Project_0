using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraViewDown : MonoBehaviour {

    public float newOffset = -0.5f;
	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            Camera.main.GetComponent<camara_movement>().changeYOffset(newOffset);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            Camera.main.GetComponent<camara_movement>().changeYOffset(0.8f);
    }
}

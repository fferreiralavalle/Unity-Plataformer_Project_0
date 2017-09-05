using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

    public float cicleTime = 2f;
	
	void FixedUpdate () {
        transform.Rotate(Vector3.up * 360 / cicleTime * Time.deltaTime);
    }
}

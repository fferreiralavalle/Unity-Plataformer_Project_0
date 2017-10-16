using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public float cicleTime = 2f;

    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * 360 / cicleTime * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour {

	public int CheckPointPriority = 1;
	void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameMaster.Instance.updateSpawn(CheckPointPriority, transform.position);
        }
    }
}

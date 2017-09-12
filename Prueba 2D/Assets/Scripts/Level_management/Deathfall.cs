using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathfall : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col)
    {
        print("Entered coll with " + col.gameObject.name);
        if (col.tag == "Player")
        {
            if (!col.gameObject.GetComponent<Player_Controller>().hasPlayerFell())
                GameMaster.Instance.handlePlayerFallToDeath();
        }
    }
}

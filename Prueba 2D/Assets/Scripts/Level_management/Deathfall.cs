using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathfall : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            GameObject player = col.gameObject;
            player.GetComponent<Player_Controller>().takeFallDamage(1);
        }
    }
}

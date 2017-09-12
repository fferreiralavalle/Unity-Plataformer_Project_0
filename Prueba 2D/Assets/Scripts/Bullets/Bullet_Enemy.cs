using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Enemy : MonoBehaviour {

    public float damageDealt = 1f;
    public string friendlyTag = "Enemy";

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            col.gameObject.GetComponent<Player_Controller>().takeDamage(damageDealt);
            col.gameObject.GetComponent<Player_Controller>().knockBack(transform.position.x);
        }
        if (col.tag != friendlyTag && col.tag!="Untagged")
        {
            Destroy(gameObject);
        }
       
    }

}

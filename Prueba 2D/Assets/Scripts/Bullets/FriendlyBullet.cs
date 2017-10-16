using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyBullet : MonoBehaviour {

    public float damageDealt = 1f;
    public string friendlyTag = "Player";

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            col.gameObject.GetComponent<Basic_Enemy>().takeDamage(damageDealt);
            Destroy(gameObject);
        }

    }
}
